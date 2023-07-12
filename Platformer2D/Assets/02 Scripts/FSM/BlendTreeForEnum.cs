using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeForEnum : StateMachineBehaviour
{
    [SerializeField] private string _currentParam;  //current값에 대한 파라미터 값
    [SerializeField] private int _max;
    [SerializeField] private float _resetTime = 0.5f;
    private int _current;
    private bool _isCorouting;
    private Coroutine _coroutine;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isCorouting)
        {
            AnimatorManager.instance.StopCoroutine(_coroutine);
            _isCorouting = false;
            _coroutine = null;
        }
        base.OnStateEnter(animator, stateInfo, layerIndex);

        animator.SetFloat(_currentParam, _current);
        _current = _current < _max ? _current + 1 : 0;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (_isCorouting == false)
        {
            _isCorouting = true; //코루틴 실행 전에 초기화해줌
            _coroutine = AnimatorManager.instance.StartCoroutine(E_Reset());
        }
    }

    IEnumerator E_Reset()
    {
        float timeMark = Time.time;  //save start time
        while (Time.time - timeMark < _resetTime)
        {
            yield return null;
        }
        _current = 0;
        _isCorouting = false;
        _coroutine = null;
    }
}
