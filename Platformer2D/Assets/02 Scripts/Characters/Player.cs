using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : Character
{
    private PlayerInput playerInput;
    /*
    //private float _horizontal;
    //private float _vertical;

    //함수 이름으로 바인딩
    //public void OnHorizontal(InputValue value)
    //{
    //   _horizontal = value.Get<float>();       
    //}

    //public void OnVertical(InputValue value)
    //{
    //    _vertical = value.Get<float>();
    //}
    
    //public void OnAttack()
    //{
    //    stateMachine.ChangeState(StateType.Attack);
    //}
    */

    protected override void Awake()
    {
        //character의 awake를 virtual로 바꾸고 오버라이드
        base.Awake();

        InputManager.Map map = new InputManager.Map();

        map.AddRawAxisAction("Horizontal", (value) =>
        {
            movement.horizontal = value;
            if (value > 0)
                movement.direction = Movement.DIRECTION_RIGHT;
            else if (value < 0)
                movement.direction = Movement.DIRECTION_LEFT;
        });
        map.AddKeyDownAction(KeyCode.Space, () => stateMachine.ChangeState(stateMachine.currentType == StateType.Crouch ? StateType.DownJump : StateType.Jump));
        map.AddKeyDownAction(KeyCode.UpArrow, () => stateMachine.ChangeState(StateType.LadderUp));
        map.AddKeyDownAction(KeyCode.DownArrow, () =>
        {
            if (stateMachine.ChangeState(StateType.LadderDown)) { }
            else if (stateMachine.ChangeState(StateType.Crouch)) { }
        });
        map.AddKeyUpAction(KeyCode.DownArrow, () => stateMachine.ChangeState(StateType.StandUp));
        map.AddKeyPressAction(KeyCode.A, () => stateMachine.ChangeState(StateType.Attack));

        InputManager.instance.AddMap("PlayerAction", map);


        /* //playerInput = GetComponent<PlayerInput>();

        //InputAction crouchAction = playerInput.currentActionMap.FindAction("Crouch");
        //crouchAction.performed += ctx => stateMachine.ChangeState(StateType.Crouch);  //performed : 눌렸을 때
        //crouchAction.canceled += ctx => stateMachine.ChangeState(StateType.StandUp);  //canceled : 취소됐을 때

        //InputAction jumpAction = playerInput.currentActionMap.FindAction("Jump");
        //jumpAction.performed += ctx 
        //    => stateMachine.ChangeState(stateMachine.currentType == StateType.Crouch ? StateType.DownJump : StateType.Jump);

        //InputAction upAction = playerInput.currentActionMap.FindAction("Up");
        //upAction.performed += ctx => stateMachine.ChangeState(StateType.LadderUp);

        //InputAction downAction = playerInput.currentActionMap.FindAction("Down");
        //downAction.performed += ctx =>
        //{
        //    if (stateMachine.ChangeState(StateType.LadderDown)) { }
        //    else if (stateMachine.ChangeState(StateType.Crouch)) { }
            
        //};
        //downAction.canceled += ctx => stateMachine.ChangeState(StateType.StandUp); */

    }

    /* //private void Update()
    //{
    //    movement.horizontal = _horizontal;
    //    //update에서 안했을 때 왼쪽눌렀는데 오른쪽으로 가고 입력들어온거 바로바로 반영안해조서 밀린듯

        
    //} */
}
