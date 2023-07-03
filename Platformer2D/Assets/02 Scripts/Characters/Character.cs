using System;
using UnityEngine;

//player와 enemy가 character를 상속받게 하려고 추상클래스로 만듦
public abstract class Character : MonoBehaviour, IHp
{
    [Header("Stats")]
    public float jumpForce = 2.5f;
    public float downJumpForce = 1.0f;
    public float landDistance = 1.0f;

    protected Movement movement;
    protected StateMachine stateMachine;

    public float hp
    {
        get => _hp;
        set
        {
            if (_hp == value)
                return;

            float prev = _hp;
            _hp = value;

            onHpChanged?.Invoke(value);
            if (prev > value)
            {
                onHpDecreased?.Invoke(prev - value);
                if(value <= _hpMin)
                {
                    onHpMin?.Invoke();
                    stateMachine.ChangeState(StateType.Die);
                }
                else
                {
                    stateMachine.ChangeState(StateType.Hurt);
                }
            }
            else
            {
                onHpIncreased?.Invoke(value - prev);
                if(value >= _hpMax)
                {
                    onHpMax?.Invoke();
                }
            }
        }
    }

    public float hpMin => _hpMin;

    public float hpMax => _hpMax;

    private float _hp;
    private float _hpMin;
    [SerializeField] private float _hpMax;

    public event Action<float> onHpChanged;
    public event Action<float> onHpDecreased;
    public event Action<float> onHpIncreased;
    public event Action onHpMin;
    public event Action onHpMax;

    protected virtual void Awake()
    {
        movement = GetComponent<Movement>();
        stateMachine = GetComponent<StateMachine>();

        //value에 따라 상태 바꾸기
        movement.onHorizontalChanged += (value) =>
        {
            StateType tmpType = value == 0.0f ? StateType.Idle : StateType.Move;
            stateMachine.ChangeState(tmpType);
        };

        //onHpDecreased += (amount) => stateMachine.ChangeState(StateType.Hurt);
        //onHpMin += () => stateMachine.ChangeState(StateType.Die);
    }

    protected virtual void Start()
    {
        hp = hpMax;
    }

    public virtual void Damage(GameObject damager, float amount)
    {
        hp -= amount;
    }

    public virtual void Heal(GameObject healer, float amount)
    {
        hp += amount;
    }
}