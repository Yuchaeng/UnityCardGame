using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems.BehaviourTree
{
    public class RandomSleep : Node
    {
        private float _time;
        public RandomSleep(BehaviourTreeBuilder tree, BlackBoard blackBoard)
            : base(tree, blackBoard)
        {
            _time = UnityEngine.Random.Range(0.0f, 1.5f);
        }

        public override Result Invoke()
        {
            tree.Sleep(_time);
            return Result.Running;
        }
    }
}