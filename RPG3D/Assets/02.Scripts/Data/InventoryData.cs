using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Data
{

    public class InventoryData : IDataModel
    {
        public int id { get; set; }

        public struct itemPair
        {
            public int id;
            public int num;
        }

        public List<itemPair> items;

        public InventoryData(int totalSlot)
        {
            items = new List<itemPair>(totalSlot);
        }
    }

}

