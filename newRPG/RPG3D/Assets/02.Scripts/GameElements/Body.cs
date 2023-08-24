using RPG.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameElements
{
    public enum BodyPart
    {
        None,
        Head,
        Top,
        Bottom,
        RightFoot,
        LeftFoot,
        RightHand,
        LeftHand,
        TwoHand
    }

    public class Body : MonoBehaviour
    {
        public List<UKeyValuePair<BodyPart, Transform>> bodyParts;
        

        private void Start()
        {
            if (DataModelManager.instance.TryGet(out ItemsEquippedData itemsEquippedData))
            {
                // Ư�� ������ ���������� ������ �ٲ������
                itemsEquippedData.slotDatum.onItemChanged += (slotIndex, slotData) =>
                {
                    // ������ �����Ȱ� ������ �ı�
                    if (bodyParts[slotIndex].Value.childCount == 1)
                    {
                        Destroy(bodyParts[slotIndex].Value.GetChild(0).gameObject);
                    }

                    // ���� ������ �� ������ �ش� �� ����
                    if (slotData.isEmpty == false)
                    {
                        Instantiate(ItemDataRepository.instance.equipments[slotData.itemID].model,
                                    bodyParts[slotIndex].Value);
                    }
                };
            }
        }
    }
}