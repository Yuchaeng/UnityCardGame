using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems.BehaviourTree
{
    public class Root : Node, IParentOfChild
    {
        public Root(BehaviourTreeBuilder tree, BlackBoard blackBoard) : base(tree, blackBoard)
        {
        }

        public Node child { get; set; }

        public override Result Invoke()
        {
            return child.Invoke();
        }
    }
}

