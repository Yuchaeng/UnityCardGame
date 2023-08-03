using RPG.Data;
using RPG.Singletons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controllers
{
    //controller 추상화, manager에게 관리하게
    public class ControllerManager : SingletonBase<ControllerManager>
    {
        public Dictionary<Type, IControllable> controllers = new Dictionary<Type, IControllable>();

        public bool TryGet<T>(out T controller) where T : IControllable
        {
            if (controllers.TryGetValue(typeof(T), out IControllable result))
            {
                controller = (T)result;
                return true;
            }

            controller = default(T);
            return false;
        }

        public void SetActive<T>(bool active)
            where T : IControllable
        {
            if (controllers.TryGetValue(typeof(T), out IControllable result))
            {
                result.controlEnabled = active;
            }

        }

        public void Register(IControllable controllable)
        {
            if (controllers.TryAdd(controllable.GetType(), controllable))
            {
                Debug.Log($"[ControllerManager] : Successfully registered {controllable.GetType()}");
            }
        }
    }
}
