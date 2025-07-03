using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FractionGame.Utility
{
    public abstract class Draggable : MonoBehaviour
    {
        private bool isHeld = false;
        public bool IsHeld { get { return isHeld; } }
        public int SortingOrder { get; set; }
        private LayerManager layerManager;

        protected virtual void Awake()
        {
            isHeld = false;
            layerManager = LayerManager.GetInstance();
            // Add self to layer manager
            layerManager.AddDraggable(this);
        }

        protected virtual void OnDestroy()
        {
            // Remove self from layer manager
            layerManager.RemoveDraggable(this);
        }

        /// <summary>
        /// Attaches current game object to pointer. Requires object to not be held.
        /// </summary>
        /// <returns>Returns false if the object is held.</returns>
        public virtual bool Attach()
        {
            // If already held, ignore
            if (isHeld) return false;
            isHeld = true;

            // Send sprite to foreground layer
            layerManager.ToLayer(gameObject, "Foreground");
            
            return true;
        }

        /// <summary>
        /// Dettaches current game object from pointer. Requires object to be held.
        /// </summary>
        /// <returns>Returns false if the object is not held.</returns>
        public virtual bool Detach()
        {
            // If not held, ignore
            if (!isHeld) return false;
            isHeld = false;

            // Send sprite to a new highest order on the default layer
            layerManager.ToLayer(gameObject, "Default");
            layerManager.ToOrder(gameObject, layerManager.NewHighestOrder());
            
            return true;
        }
    }
}