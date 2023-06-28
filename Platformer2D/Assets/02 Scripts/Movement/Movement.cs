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
            if(value < 0)  //���� ������ ����. �������� �⺻ �������� ��������
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
            //onHorizontalChanged(value); //����ȣ��, ��ϵ� �Լ��� ȣ���� ������ ���ڸ� �����ؼ� ��� value �����Ǹ� ������ value�� �����ϰԵ� �߰��� ������ٲ����
            //onHorizontalChanged.Invoke(value); //����ȣ��, ���ڸ� �����س���(���� ����) ��ϵ� �Լ����� ����� ���� ����(set�� ���ÿ� ����) value�� �ٲ��� ����� �ȹٲ�
            //Invoke�� �Ű������� ���� ���� �� ��ϵ� �Լ����� Invoke�� �Ű������� ������
            //���������� �ѹ� �� ���� Invoke��� �Լ� ȣ�� ������ �Ѱ� �� �׿���
            onHorizontalChanged?.Invoke(value); //null üũ ������ - null�̸�(��ϵ� �Լ� ������) ȣ�� x
        
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

            //direction = _horizontal < 0 ? DIRECTION_LEFT : DIRECTION_RIGHT; //�������� ���� ����Ű ������ �ٽ� ������ ��
        }
        
        

    }

    private void FixedUpdate()
    {
        _rigidbody.position += _move * _speed * Time.fixedDeltaTime;
    }
}
