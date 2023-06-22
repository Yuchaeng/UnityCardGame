using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.U2D;

public class StateMachine : MonoBehaviour
{
    public StateType currentType;
    public IStateEnumerator<StateType> current;
    //public State current;  //원래 이거였는데 stateMachine이 굳이 State를 참조하기보다는 ISateEnumerator참조하는게
    //더 나아서? 바꿈 (인터페이스가 덜 변화함, state 참조해도 reset current 이 정도만 써서)
    public Dictionary<StateType, IStateEnumerator<StateType>> states;  // <어떤 상태일 때, 어떤 workflow>

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


    public void InitStates(Dictionary<StateType, IStateEnumerator<StateType>> states)  //start에서 초기화하다가 character에서 호출하는 형태로 바꿈
    {
        this.states= states;

        //states = new Dictionary<StateType, State>
        //{
        //    { StateType.Idle, new StateIdle(this) },
        //    { StateType.Move, new StateMove(this) },
        //};

        current = states[currentType];

        //fsm - idle, move 등등 만들어서 이렇게 추가하는 형식
        //states.Add(StateType.Idle, new StateIdle(this));  //이거 단순화하면 위처럼 표현
    }
}
