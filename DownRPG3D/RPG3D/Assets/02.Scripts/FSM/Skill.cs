using RPG.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.FSM
{
    public class Skill : BehaviourBase
    {
        public SkillID skillID;
        protected ActiveSkillData data;
        [SerializeField] private bool _hasExitTime;
        [SerializeField] private StateType _destination;
        public int comboStack;
        private int _comboStackAnimHashID;
        private bool _isCorouting;
        private Coroutine _coroutine;

        public override void Initialize(MachineManager manager)
        {
            base.Initialize(manager);
            _comboStackAnimHashID = Animator.StringToHash("comboStack");
            data = SkillDataRepository.instance.activeSkillDatum[skillID.value];

        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            if (_isCorouting)
            {
                manager.StopCoroutine(_coroutine);
                _isCorouting = false;
            }

            comboStack = comboStack < data.comboStackMax ? comboStack + 1 : 0;
            animator.SetInteger(_comboStackAnimHashID, comboStack);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            //Debug.Log($"update ... {stateInfo.normalizedTime}");

            if (stateInfo.normalizedTime >= data.comboDelayRatioList[comboStack] &&
                manager.skillCastingDoneFlags[skillID.value] == false)
            {
                // todo -> �޺� ���� �����ϵ���
                manager.skillCastingDoneFlags[skillID.value] = true;
            }

            if (_hasExitTime &&
                stateInfo.normalizedTime >= 1.0f)
            {
                manager.ChangeState(_destination);
            }
        }

      
    }
}