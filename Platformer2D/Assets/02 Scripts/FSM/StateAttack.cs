using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : State
{
    public override bool canExecute => machine.currentType == StateType.Idle ||
                                       machine.currentType == StateType.Move;
    //idle이나 move일 때만 attack 가능하다는 뜻, canExecute 먼저 체크하고 조건 통과되면 상태바꿈

    public StateAttack(StateMachine machine) : base(machine)
    {
    }

    public override StateType MoveNext()
    {
        StateType next = StateType.Attack;

        switch (currentStep)
        {
            case IStateEnumerator<StateType>.Step.None:
                {
                    movement.isMovable = false;
                    movement.isDiretionChangeable = false;
                    animator.Play("Attack");
                    currentStep++;
                }
                break;
            case IStateEnumerator<StateType>.Step.Start:
                {
                    
                    currentStep++;
                }
                break;
            case IStateEnumerator<StateType>.Step.Casting:
                {
                    currentStep++;
                }
                break;
            case IStateEnumerator<StateType>.Step.DoAction:
                {
                    currentStep++;
                }
                break;
            case IStateEnumerator<StateType>.Step.WaitUntilActionFinished:
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        currentStep++;
                    }
                }
                break;
            case IStateEnumerator<StateType>.Step.Finish:
                {
                    //next = StateType.Idle;  //이렇게하면 방향키눌러도 idle거치고 move로 변함 
                    next = movement.horizontal == 0.0f ? StateType.Idle : StateType.Move;
                }
                break;
            default:
                break;
        }

        return next;
    }
}
