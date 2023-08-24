﻿using RPG.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.UI
{
    public class UIManager : SingletonBase<UIManager>
    {
        public Dictionary<Type, IUI> uis = new Dictionary<Type, IUI>();
        public LinkedList<IUI> uisShown = new LinkedList<IUI>();

        public void Register(IUI ui)
        {
            if (uis.TryAdd(ui.GetType(), ui) == false)
                throw new Exception($"[UIManager] : Failed to register {ui.GetType()}");
        }

        public bool TryGet<T>(out T ui) where T : IUI
        {
            if (uis.TryGetValue(typeof(T), out IUI value))
            {
                ui = (T)value;
                return true;
            }

            ui = default(T);
            return false;
        }

        public void Push(IUI ui)
        {
            if (uisShown.Count > 0 &&
                uisShown.Last.Value == ui)
                return;

            int sortingOrder = 0;
            if (uisShown.Last != null)
            {
                uisShown.Last.Value.inputActionEnalbed = false;
                sortingOrder = uisShown.Last.Value.sortingOrder;
            }
            uisShown.Remove(ui);
            uisShown.AddLast(ui);
            ui.inputActionEnalbed = true;
            ui.sortingOrder = sortingOrder;
        }

        public void Pop(IUI ui)
        {
            uisShown.Remove(ui);

            if (uisShown.Count > 0)
            {
                uisShown.Last.Value.inputActionEnalbed = true;
            }
        }

        public void HideLast()
        {
            if (uisShown.Count <= 0)
                return;

            uisShown.Last.Value.Hide();
        }
    }
}