using RPG.GameElements;
using RPG.UI;
using UnityEngine;

namespace RPG.Data
{
    [CreateAssetMenu(fileName = "new EquipmentItemData", menuName = "RPG/Data/Create EquipmentItemData")]
    public class EquipmentItemData : UsableItemData
    {
        public BodyPart bodyPart;

        public void Use(InventorySlot slot)
        {
            if (DataModelManager.instance.TryGet(out InventoryData inventoryData) &&
                DataModelManager.instance.TryGet(out ItemsEquippedData itemsEquippedData))
            {
                EquipmentItemData equippedItemData = ItemDataRepository.instance[slot.itemID] as EquipmentItemData;

                //장착 되어있던거
                ItemsEquippedData.ItemEquippedSlotData equippedSlotData = itemsEquippedData.slotDatum[(int)equippedItemData.bodyPart];
                //장착 하려고 하는거
                InventoryData.EquipmentSlotData inventorySlotData = inventoryData.equipmentSlotDatum[slot.slotIndex];

                //장착하고있던게 기존에 있으면
                //장착하려는 장비가 있던 인벤토리 슬롯에 채워넣음
                if (equippedItemData != null)
                {
                    inventoryData.equipmentSlotDatum[slot.slotIndex] = new InventoryData.EquipmentSlotData()
                    {
                        itemID = equippedSlotData.itemID,
                        itemNum = 1,
                        enhanceLevel = equippedSlotData.enhanceLevel
                    };
                }

                //장착하려는 인벤토리 슬롯의 장비를 장비창 데이터에 씀
                itemsEquippedData.slotDatum[(int)equippedItemData.bodyPart] = new ItemsEquippedData.ItemEquippedSlotData()
                {
                    part = equippedItemData.bodyPart,
                    itemID = inventorySlotData.itemID,
                    enhanceLevel = equippedSlotData.enhanceLevel

        };
            }
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
