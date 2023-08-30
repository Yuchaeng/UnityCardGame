using RPG.GameElements.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour�� ���� ��ӹ��� �ʿ�� ���� �׳� ���������ڸ� ��� �������־���ϴ����� ����
// �����͸� ScriptableObject ���� ���� ����Ʈ�� �־�ΰ� ��Ÿ�ӿ� StatModifier �����ؼ� �ᵵ ��

public enum StatModifyingOption
{
    None,
    AddFlat,
    AddPercent,
    MulPercent,
}

[Serializable]
public class StatModifier
{
    public StatType type;
    public StatModifyingOption modifyingOption;
    public float value;
}
