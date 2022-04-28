using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core.Unit
{
    public class MoveCommand : IMoveCommand
    {
        public Vector3 Target { get; }

        public MoveCommand(Vector3 target)
        {
            Target = target;
        }
    }
}