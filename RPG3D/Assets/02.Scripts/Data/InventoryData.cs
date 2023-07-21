﻿using RPG.Collections;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

namespace RPG.Data
{

    public class InventoryData : IDataModel
    {
        public int id { get; set; }

        public abstract class ItemSlotData
        {
            public int itemID;
            public int itemNum;
        }

        public class EquipmentSlotData : ItemSlotData
        {
            public int enhanceLevel;  //강화 레벨
        }

        public class SpendSlotData : ItemSlotData
        {

        }

        public class ETCSlotData : ItemSlotData
        {

        }

        public ObservableCollection<EquipmentSlotData> equipmentSlotDatum;
        public ObservableCollection<SpendSlotData> spendSlotDatum;
        public ObservableCollection<ETCSlotData> etcSlotDatum;
        

        /*public InventoryData(int totalSlot)
        {
            items = new List<itemPair>(totalSlot);
        }*/
    }

}

