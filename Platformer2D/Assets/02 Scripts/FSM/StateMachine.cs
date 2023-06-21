using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.U2D;

public class StateMachine : MonoBehaviour
{
    public StateType currentType;
    public State current;
    public Dictionary<StateType, State> states;

    public bool ChangeState(StateType newType)
    {
        if (currentType == newType)
            return false;

        states[currentType].Reset();
        current = states[newType];
        currentType = newType;
        return true;
    }

    private void Update()
    {
        ChangeState(current.MoveNext());
    }

    private void Start()
    {
        InitStates();
    }

    private void InitStates()
    {
        states = new Dictionary<StateType, State>
        {
            { StateType.Idle, new StateIdle(this) },
            { StateType.Move, new StateIdle(this) },
        };

        current = states[currentType];

        //fsm - idel, move ��� ���� �̷��� �߰��ϴ� ����
        //states.Add(StateType.Idle, new StateIdle(this));  //�̰� �ܼ�ȭ�ϸ� ��ó�� ǥ��
    }
}
