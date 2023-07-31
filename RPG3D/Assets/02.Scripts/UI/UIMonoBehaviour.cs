﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.UI
{
    [RequireComponent(typeof(Canvas))]
    public class UIMonoBehaviour : MonoBehaviour, IUI
    {
        protected UIManager manager;
        protected Canvas canvas;
        [Header("Options")]
        [SerializeField] private bool hideWhenClickOutside;

        public int sortingOrder
        {
            get => canvas.sortingOrder;
            set => canvas.sortingOrder = value;
        }

        public event Action onShow;
        public event Action onHide;

        public void Show()
        {
            manager.Push(this);
            gameObject.SetActive(true);
            onShow?.Invoke();
        }

        public void Hide()
        {
            manager.Pop(this);
            gameObject.SetActive(false);
            onHide?.Invoke();
        }

        public void ShowUnmanaged()
        {
            gameObject.SetActive(true);
            onShow?.Invoke();
        }

        public void HideUnmanaged()
        {
            gameObject.SetActive(false);
            onHide?.Invoke();
        }

        public virtual void InputAction()
        {

        }

        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
            manager = UIManager.instance;
            manager.Register(this);

            if (hideWhenClickOutside)
            {
                GameObject panel = new GameObject();
                Image image = panel.AddComponent<Image>();
                image.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
                
                panel.transform.SetParent(transform);
                panel.transform.SetAsFirstSibling();

                RectTransform rect = panel.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0.0f, 0.0f);  //bottom left
                rect.anchorMax = new Vector2(1.0f, 1.0f);  //right top
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;
                rect.sizeDelta = Vector2.zero;

                EventTrigger eventTrigger = panel.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener(data => Hide());
                eventTrigger.triggers.Add(entry);
            }
        }
    }
}
