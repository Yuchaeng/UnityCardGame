using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDownJump : State
{
    public override bool canExecute => _groundDetector.isDetected && machine.currentType == StateType.Crouch;

    private GroundDetector _groundDetector;

    public StateDownJump(StateMachine machine) : base(machine)
    {
        _groundDetector = machine.GetComponent<GroundDetector>();
    }

    public override StateType MoveNext()
    {
        StateType next = StateType.DownJump;

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
                    animator.Play("Jump");
                    rigidBody.AddForce(Vector2.up * character.downJumpForce, ForceMode2D.Impulse);
                    _groundDetector.IgnoreLatest(collider);
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
                    if (rigidBody.velocity.y <= 0)
                    {
                        currentStep++;
                    }

                }
                break;
            case IStateEnumerator<StateType>.Step.Finish:
                {
                    if (_groundDetector.isDetected)
                        next = movement.horizontal == 0.0f ? StateType.Idle : StateType.Move;
                    else
                        next = StateType.Fall;
                }
                break;
            default:
                break;
        }

        return next;
    }
}
