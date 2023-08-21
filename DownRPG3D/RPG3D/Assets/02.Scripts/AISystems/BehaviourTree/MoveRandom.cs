using UnityEditor.AnimatedValues;
using UnityEngine;

namespace RPG.AISystems.BehaviourTree
{
    public class MoveRandom : Node
    {
        private float _min;
        private float _max;
        private GameObject _gameObject;
        


        public MoveRandom(BehaviourTreeBuilder tree, BlackBoard blackBaord, float min, float max, GameObject gameObject)
            : base(tree, blackBaord)
        {
            _min = min;
            _max = max;
            _gameObject = gameObject;
        }


        public override Result Invoke()
        {
            bool result = false;
            Vector3 destination;

            destination = new Vector3(Random.Range(_min, _max), Random.Range(_min, _max), Random.Range(_min, _max));

            if (destination != _gameObject.transform.position)
            {
                result = true;
            }

            if (result)
            {
                blackBoard.agent.destination = destination;
            }

            return result ? Result.Success : Result.Failure;
        }
    }
}