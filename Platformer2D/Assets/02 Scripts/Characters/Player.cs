using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : Character
{
    private float _horizontal;
    private float _vertical;
    public void OnHorizontal(InputValue value)
    {
       _horizontal = value.Get<float>();       
    }

    public void OnVertical(InputValue value)
    {
        _vertical = value.Get<float>();
    }
    
    public void OnAttack()
    {
        stateMachine.ChangeState(StateType.Attack);
    }

    public void OnJump()
    {
        stateMachine.ChangeState(StateType.Jump);
    }

    private void Update()
    {
        movement.horizontal = _horizontal;
        //update에서 안했을 때 왼쪽눌렀는데 오른쪽으로 가고 입력들어온거 바로바로 반영안해조서 밀렸던 것 같음

        if (_horizontal > 0)
            movement.direction = Movement.DIRECTION_RIGHT;
        else if (_horizontal < 0)
            movement.direction = Movement.DIRECTION_LEFT;
    }
}
