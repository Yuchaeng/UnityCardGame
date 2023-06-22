using UnityEngine;

//player와 enemy가 character를 상속받게 하려고 추상클래스로 만듦
public abstract class Character : MonoBehaviour
{
    protected Movement movement;
    protected StateMachine stateMachine;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        stateMachine = GetComponent<StateMachine>();

        //value에 따라 상태 바꾸기
        movement.onHorizontalChanged += (value) =>
        {
            StateType tmpType = value == 0.0f ? StateType.Idle : StateType.Move;
            stateMachine.ChangeState(tmpType);
        };
    }
}