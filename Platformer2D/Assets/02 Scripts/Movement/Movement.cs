using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public bool isMovable;
    public bool isDiretionChangeable;

    public const int DIRECTION_RIGHT = 1;
    public const int DIRECTION_LEFT = -1;
    public int direction
    {
        get => _direction;
        set
        {
            if(value < 0)  //음수 들어오면 왼쪽. 오른쪽을 기본 방향으로 설정했음
            {
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                _direction = DIRECTION_LEFT;
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
                _direction = DIRECTION_RIGHT;
            }
        }
    }

    private int _direction;
    public float horizontal
    {
        get => _horizontal;
        set
        {
            if (isMovable == false)
                return;

            if (_horizontal == value)
                return;

            _horizontal = value;
            //onHorizontalChanged(value); //직접호출, 등록된 함수를 호출할 때마다 인자를 참조해서 사용 value 수정되면 수정된 value를 참조하게됨 중간에 결과값바뀔수도
            //onHorizontalChanged.Invoke(value); //간접호출, 인자를 복사해놓고(따로 저장) 등록된 함수들은 복사된 값을 참조(set은 스택에 저장) value값 바껴도 결과값 안바뀜
            //Invoke의 매개변수에 인자 전달 후 등록된 함수들은 Invoke의 매개변수를 참조함
            //지역변수가 한번 더 생김 Invoke라는 함수 호출 스택이 한개 더 쌓여서
            onHorizontalChanged?.Invoke(value); //null 체크 연산자 - null이면(등록된 함수 없으면) 호출 x
        
        }
    }
    private float _horizontal;
    public event Action<float> onHorizontalChanged;

    private Rigidbody2D _rigidbody;
    private Vector2 _move;
    [SerializeField] private float _speed = 1.0f;

    private void Awake()
    {
        _rigidbody= GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        //_move = isMovable ? new Vector2(horizontal, 0.0f) : Vector2.zero;

        if (isMovable)
        {
            _move = new Vector2(horizontal, 0.0f);
        }
        else
        {
            _move = Vector2.zero;
        }

        if (isDiretionChangeable)
        {

            if (_horizontal > 0)
                direction = DIRECTION_RIGHT;
            else if(_horizontal< 0)
                direction = DIRECTION_LEFT;

            //direction = _horizontal < 0 ? DIRECTION_LEFT : DIRECTION_RIGHT; //왼쪽으로 가다 방향키 놓으면 다시 오른쪽 봄
        }
        
        

    }

    private void FixedUpdate()
    {
        _rigidbody.position += _move * _speed * Time.fixedDeltaTime;
    }
}
