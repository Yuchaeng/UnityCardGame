using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.U2D;

public class StateMachine : MonoBehaviour
{
    public StateType currentType;
    public IStateEnumerator<StateType> current;
    //public State current;  //���� �̰ſ��µ� stateMachine�� ���� State�� �����ϱ⺸�ٴ� ISateEnumerator�����ϴ°�
    //�� ���Ƽ�? �ٲ� (�������̽��� �� ��ȭ��, state �����ص� reset current �� ������ �Ἥ)
    public Dictionary<StateType, IStateEnumerator<StateType>> states;  // <� ������ ��, � workflow>

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


    public void InitStates(Dictionary<StateType, IStateEnumerator<StateType>> states)  //start���� �ʱ�ȭ�ϴٰ� character���� ȣ���ϴ� ���·� �ٲ�
    {
        this.states= states;

        //states = new Dictionary<StateType, State>
        //{
        //    { StateType.Idle, new StateIdle(this) },
        //    { StateType.Move, new StateMove(this) },
        //};

        current = states[currentType];

        //fsm - idle, move ��� ���� �̷��� �߰��ϴ� ����
        //states.Add(StateType.Idle, new StateIdle(this));  //�̰� �ܼ�ȭ�ϸ� ��ó�� ǥ��
    }
}
