using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
   
    private void Start()
    {
        //드래그앤드롭 안하고 스크립트에서 바로 넣어주는 방법
        Enemy target = GetComponentInParent<Enemy>();
        Slider hpBar = GetComponentInChildren<Slider>();

        hpBar.minValue = 0.0f;
        hpBar.maxValue = target.hpMax;
        hpBar.value = target.hp;

        target.onHpChanged += (value) =>
        {
            hpBar.value = value;
        };

        //flip 쓰면 따로 이렇게 안해도 됨
        Movement movement = target.GetComponent<Movement>();
        movement.onDirectionChanged += (value) =>
        {
            transform.localEulerAngles = value > 0 ? Vector3.zero : new Vector3(0.0f, 180.0f, 0.0f);
        };
    }
}
