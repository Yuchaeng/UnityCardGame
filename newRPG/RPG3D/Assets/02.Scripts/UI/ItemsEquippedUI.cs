using RPG.Data;
using RPG.DependencySources;
using RPG.GameElements;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class ItemsEquippedUI : UIMonoBehaviour
    {
        private ItemsEquippedPresenter _presenter;
        public Dictionary<BodyPart, ItemEquippedSlot> slots;
        [SerializeField] private Button _close;

        public override void InputAction()
        {
            base.InputAction();

            ItemEquippedSlot slot;

            if (Input.GetMouseButtonDown(1))
            {
                if (inputModule.TryGetHovered<GraphicRaycaster, ItemEquippedSlot>(out slot))
                {
                    if (_presenter.unequipCommand.TryExecute(slot))
                    {
                        Debug.Log($"[ItemEquippedUI] : Unequipped body part of {slot.bodyPart}");
                    }
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            slots = new Dictionary<BodyPart, ItemEquippedSlot>();
            foreach (var slot in GetComponentsInChildren<ItemEquippedSlot>())
            {
                slots.Add(slot.bodyPart, slot);
            }


            _presenter = new ItemsEquippedPresenter();

            for (int i = 0; i < _presenter.itemsEquippedSource.itemsEquippedSlotDatum.Count; i++)
            {
                BodyPart bodyPart = (BodyPart)i;
                ItemsEquippedData.ItemEquippedSlotData slotData
                    = _presenter.itemsEquippedSource.itemsEquippedSlotDatum[i];

                Debug.Log($"Equipped {slotData.itemID} on {bodyPart}");
                switch (bodyPart)
                {
                    case BodyPart.Head:
                    case BodyPart.Top:
                    case BodyPart.Bottom:
                    case BodyPart.RightFoot:
                        {
                            slots[bodyPart].Refresh(slotData.itemID);
                        }
                        break;
                    case BodyPart.LeftFoot:
                        break;
                    case BodyPart.RightHand:
                        {
                            slots[bodyPart].Refresh(slotData.itemID);
                            slots[BodyPart.LeftHand].Refresh(_presenter.itemsEquippedSource.itemsEquippedSlotDatum[(int)BodyPart.LeftHand].itemID);
                        }
                        break;
                    case BodyPart.LeftHand:
                        {
                            slots[bodyPart].Refresh(slotData.itemID);
                            slots[BodyPart.RightHand].Refresh(_presenter.itemsEquippedSource.itemsEquippedSlotDatum[(int)BodyPart.RightHand].itemID);
                        }
                        break;
                    case BodyPart.TwoHand:
                        {
                            slots[BodyPart.RightHand].Refresh(slotData.itemID);
                            slots[BodyPart.LeftHand].Shadow(slotData.itemID);
                        }
                        break;
                    default:
                        break;
                }
            }

            _presenter.itemsEquippedSource.itemsEquippedSlotDatum.onItemChanged += (slotIndex, slotData) =>
            {
                BodyPart bodyPart = (BodyPart)slotIndex;

                Debug.Log($"Equipped {slotData.itemID} on {bodyPart}");
                switch (bodyPart)
                {
                    case BodyPart.Head:
                    case BodyPart.Top:
                    case BodyPart.Bottom:
                    case BodyPart.RightFoot:
                        {
                            slots[bodyPart].Refresh(slotData.itemID);
                        }
                        break;
                    case BodyPart.LeftFoot:
                        break;
                    case BodyPart.RightHand:
                        {
                            slots[bodyPart].Refresh(slotData.itemID);
                            slots[BodyPart.LeftHand].Refresh(_presenter.itemsEquippedSource.itemsEquippedSlotDatum[(int)BodyPart.LeftHand].itemID);
                        }
                        break;
                    case BodyPart.LeftHand:
                        {
                            slots[bodyPart].Refresh(slotData.itemID);
                            slots[BodyPart.RightHand].Refresh(_presenter.itemsEquippedSource.itemsEquippedSlotDatum[(int)BodyPart.RightHand].itemID);
                        }
                        break;
                    case BodyPart.TwoHand:
                        {
                            slots[BodyPart.RightHand].Refresh(slotData.itemID);
                            slots[BodyPart.LeftHand].Shadow(slotData.itemID);
                        }
                        break;
                    default:
                        break;
                }
            };


            _close.onClick.AddListener(Hide);
        }
    }
}