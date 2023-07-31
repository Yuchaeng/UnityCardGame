using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.EventSystems
{
    public class CustomInputModule : StandaloneInputModule
    {

        //component 받아오는걸 시도
        public bool TryGetHovered<T, K>(out K hovered, int mouseID = kMouseLeftId)
            where T : BaseRaycaster
        {
            if (m_PointerData.TryGetValue(mouseID, out PointerEventData pointerEventData))
            {
                BaseRaycaster module = pointerEventData.pointerCurrentRaycast.module;

                if (module && module is T)
                {
                    foreach (var item in pointerEventData.hovered)
                    {
                        if (item.TryGetComponent(out hovered))
                        {
                            return true;
                        }
                    }
                }
            }

            hovered = default(K);
            return false;
        }

        public bool TryGetHovered<T>(out List<GameObject> hovered, int mouseID = kMouseLeftId)
            where T : BaseRaycaster   //ui - graphicRaycaster, world - physicsRaycaster
        {
            hovered = null;
            if (m_PointerData.TryGetValue(mouseID, out PointerEventData pointerEventData))
            {
                BaseRaycaster module = pointerEventData.pointerCurrentRaycast.module;

                if (module && module is T)
                {
                    hovered = pointerEventData.hovered;
                    return true;
                }
            }

            return false;
        }
    }
}

