using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.AISystems.BehaviourTree;
using RPG.FSM;
using UnityEngine.AI;

public class EnemyMachineManager : MachineManager
{
    private BehaviourTreeBuilder _behaviourTree;
    private NavMeshAgent _agent;
    private RandomSleep _randomSleep;

    [Header("AI")]
    [SerializeField] private float _seek_radius;
    [SerializeField] private float _seek_angle;
    [SerializeField] private float _seek_deltaAngle;
    [SerializeField] private LayerMask _seek_targetMask;
    [SerializeField] private Vector3 _seek_offset;
    [SerializeField] private float _attack_range;


    protected override void Awake()
    {
        base.Awake();
        _behaviourTree = new BehaviourTreeBuilder();
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = new Vector3(Random.Range(0.0f, 10.0f), Random.Range(0.0f, 10.0f), Random.Range(0.0f, 10.0f));
        _randomSleep = new RandomSleep(_behaviourTree, _behaviourTree.blackBoard);

        _behaviourTree.StartBuild(gameObject)
            .Selector()
                .Parallel(Parallel.Policy.RequireOne)
                    .Seek(_seek_radius, _seek_angle, _seek_deltaAngle, _seek_targetMask, _seek_offset)
                    .Condition(() => _behaviourTree.blackBoard.target != null &&
                                     Vector3.Distance(_behaviourTree.blackBoard.target.position, transform.position) < _attack_range)
                        .Execution(() => ChangeState(StateType.Attack) ? Result.Success : Result.Failure)
                .Sequence()
                    .MoveRandom(0.0f, 10.0f, gameObject)
                    .Execution(() => _randomSleep.Invoke());

        
    }

    protected override void Update()
    {
        base.Update();
        _behaviourTree.Tick();
    }
}
