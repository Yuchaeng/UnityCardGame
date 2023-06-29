using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateFall : State
{
    public override bool canExecute => machine.currentType == StateType.Idle ||
                                       machine.currentType == StateType.Move ||
                                       machine.currentType == StateType.Jump;
    //detected이면서 idle 또는 move
    private GroundDetector _groundDetector;
    private float _startPosY;

    public StateFall(StateMachine machine) : base(machine)
    {
        _groundDetector = machine.GetComponent<GroundDetector>();
    }

    public override StateType MoveNext()
    {
        StateType next = StateType.Fall;

        switch (currentStep)
        {
            case IStateEnumerator<StateType>.Step.None:
                {
                    currentStep++;
                }
                break;
            case IStateEnumerator<StateType>.Step.Start:
                {
                    movement.isMovable = false;
                    movement.isDiretionChangeable = true;
                    animator.Play("Fall");
                    _startPosY = rigidBody.position.y;  //rigidbody나 transform이나 읽는건 같은데 쓰는건 다름
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
                    if (_groundDetector.isDetected)
                    {
                        currentStep++;
                    }

                }
                break;
            case IStateEnumerator<StateType>.Step.Finish:
                {
                    if (_startPosY - rigidBody.position.y < character.landDistance)
                        next = movement.horizontal == 0.0f ? StateType.Idle : StateType.Move;
                    else
                        next = StateType.Land; //높은데서 떨어진거
                }
                break;
            default:
                break;
        }

        return next;
    }
}
