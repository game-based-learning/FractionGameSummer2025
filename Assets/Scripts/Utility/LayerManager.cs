using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FractionGame.Utility
{
    public class LayerManager
    {
        private static LayerManager instance;
        private List<Draggable> draggables;
        private int highestSortingOrder;
        // Constant value to limit the sorting order value
        private const int MAX_SORTING_ORDER = 5000;

        private LayerManager()
        {
            draggables = new List<Draggable>();
            highestSortingOrder = 0;
        }

        public void AddDraggable(Draggable item)
        {
            draggables.Add(item);
            item.SortingOrder = NewHighestOrder();
        }

        public void RemoveDraggable(Draggable item)
        {
            draggables.Remove(item);
        }
        
        /**
         * Increments new highest order and returns that value.
         */
        public int NewHighestOrder()
        {
            highestSortingOrder++;
            if (highestSortingOrder > MAX_SORTING_ORDER)
            {
                RenormalizeSortingOrder();
            }
            return highestSortingOrder;
        }

        /**
         * Set the rendering layer of the object to a specific layer
         * 0 -> Background, 1 -> Default, 2 -> Foreground
         */
        public void ToLayer(GameObject gameObject, string layerName)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = layerName;
        }

        public void ToOrder(GameObject gameObject, int order)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = order;
        }

        /**
         * Resets sorting order to start from 0
         */
        private void RenormalizeSortingOrder()
        {
            List<Draggable> normInstances = draggables.OrderBy(item => item.SortingOrder).ToList();
            highestSortingOrder = normInstances.Count;

            for (int i = 0; i < highestSortingOrder; i++)
            {
                normInstances[i].SortingOrder = i;
            }
        }

        public static LayerManager GetInstance()
        {
            // returns singleton instance of this obejct
            if (instance == null) instance = new LayerManager();
            return instance;
        }
    }
}