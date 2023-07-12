using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBase<T> : MonoBehaviour
    where T : SingletonMonoBase<T>
{
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject(nameof(T)).AddComponent<T>();  //without where keyword -> error, ex. int X, under component O 
            }
            return _instance;
        }
    }
    private static T _instance;
}
