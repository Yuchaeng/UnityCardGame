using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.AISystems.BehaviourTree;
using RPG.FSM;

public class EnemyMachineManager : MachineManager
{
    private BehaviourTreeBuilder _behaviourTree;

    [Header("AI")]
    [SerializeField] private float _seek_radius;
    [SerializeField] private float _seek_angle;
    [SerializeField] private float _seek_deltaAngle;
    [SerializeField] private LayerMask _seek_targetMask;
    [SerializeField] private Vector3 _seek_offset;
    [SerializeField] private float _attack_range;
    [SerializeField] private float _patrol_range;
    [SerializeField] private float _patrol_minTime;
    [SerializeField] private float _patrol_maxTime;


    protected override void Awake()
    {
        base.Awake();
        _behaviourTree = new BehaviourTreeBuilder();
        _behaviourTree.StartBuild(gameObject)
            .Selector()
                .Parallel(Parallel.Policy.RequireOne)
                    .Seek(_seek_radius, _seek_angle, _seek_deltaAngle, _seek_targetMask, _seek_offset)
                    .Condition(() => _behaviourTree.blackBoard.target != null &&
                                     Vector3.Distance(_behaviourTree.blackBoard.target.position, transform.position) < _attack_range)
                        .Execution(() => ChangeState(StateType.Attack) ? Result.Success : Result.Failure)
                    .ExitCurrentComposite()
                .Sequence()
                    .Condition(() =>
                    {
                        BlackBoard blackBoard = _behaviourTree.blackBoard;
                        if (blackBoard.target == null &&
                            blackBoard.agent.remainingDistance <= blackBoard.agent.stoppingDistance)
                        {
                            return true;
                        }
                        return false;
                    })
                        .Patrol(_patrol_range);
                    //.RandomSleep(_patrol_minTime, _patrol_maxTime);
    }

    protected override void Update()
    {
        base.Update();
        _behaviourTree.Tick();
    }
}
