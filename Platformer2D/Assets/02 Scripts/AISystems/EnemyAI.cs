using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(StateMachine), typeof(EnemyMovement), typeof(CapsuleCollider2D))]  //해당 컴포넌트들 추가안되어있으면 자동으로 추가해주는 기능
public class EnemyAI : MonoBehaviour
{
    public enum Step
    {
        Idle,
        Think,
        TakeARest,
        MoveLeft,
        MoveRight,
        StartFollow,
        Follow,
        StartAttack,
        Attak,
    }
    [SerializeField] private Step _step;
    [SerializeField] private bool _autoFollow;
    [SerializeField] private LayerMask _detectMask;
    [SerializeField] private float _detectRange = 1.5f;
    [SerializeField] private bool _attackEnable;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _thinkTimeMin = 0.1f;
    [SerializeField] private float _thinkTimeMax = 2.0f;
    [SerializeField] private float _thinkTimer;
    public GameObject target;
    private StateMachine _stateMachine;
    private EnemyMovement _movement;
    private CapsuleCollider2D _collider;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _movement = GetComponent<EnemyMovement>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        Collider2D detected = Physics2D.OverlapCircle(transform.position, _detectRange, _detectMask); //겹치는 부분 감지, rigidbody나 collider 컴포넌트없어도됨
        target = detected ? detected.gameObject : null; //유니티 오브젝트 기반타입들 널체크를 bool 타입으로 가능->따로 널체크안해도 오류안남

        if (_autoFollow && _step < Step.StartFollow && target)
        {
            _step = Step.StartFollow;
        }

        switch (_step)
        {
            case Step.Idle:
                break;
            case Step.Think:
                {
                    _step = (Step)Random.Range((int)Step.TakeARest, (int)Step.MoveRight + 1);
                    _thinkTimer = Random.Range(_thinkTimeMin, _thinkTimeMax);
                    
                    _stateMachine.ChangeState(_step == Step.TakeARest ? StateType.Idle : StateType.Move);
                    
                }
                break;
            case Step.TakeARest:
                {
                    _movement.horizontal = 0.0f;
                    if (_thinkTimer > 0)
                        _thinkTimer -= Time.deltaTime;
                    else
                        _step = Step.Think;
                }
                break;
            case Step.MoveLeft:
                {
                    _movement.direction = Movement.DIRECTION_LEFT;
                    _movement.horizontal = -1.0f;
                    if (_thinkTimer > 0)
                        _thinkTimer -= Time.deltaTime;
                    else
                        _step = Step.Think;
                }
                break;
            case Step.MoveRight:
                {
                    _movement.direction = Movement.DIRECTION_RIGHT;
                    _movement.horizontal = 1.0f;
                    if (_thinkTimer > 0)
                        _thinkTimer -= Time.deltaTime;
                    else
                        _step = Step.Think;
                }
                break;
            case Step.StartFollow:
                {
                    _stateMachine.ChangeState(StateType.Move);
                    _step = Step.Follow;
                }
                break;
            case Step.Follow:
                {
                    if(target == null)
                    {
                        _step = Step.Think;
                        return;
                    }

                    if(transform.position.x < target.transform.position.x - _collider.size.x)
                    {
                        _movement.direction = Movement.DIRECTION_RIGHT;
                        _movement.horizontal = 1.0f;
                    }
                    else if (transform.position.x > target.transform.position.x + _collider.size.x)
                    {
                        _movement.direction = Movement.DIRECTION_LEFT;
                        _movement.horizontal = -1.0f;
                    }

                    if (_attackEnable && Vector2.Distance(transform.position, target.transform.position) < _attackRange)
                    {
                        _step = Step.StartAttack;
                    }

                }
                break;
            case Step.StartAttack:
                {
                    _stateMachine.ChangeState(StateType.Attack);
                    _step = Step.Attak;
                }
                break;
            case Step.Attak:
                {
                    if (_stateMachine.currentType != StateType.Attack)
                        _step = Step.Think;
                }
                break;
            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if (_autoFollow)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _detectRange);
        }

        if (_attackEnable)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}
