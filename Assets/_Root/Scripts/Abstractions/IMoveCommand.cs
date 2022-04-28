using UnityEngine;

namespace Abstractions
{
    public interface IMoveCommand : ICommand
    {
        public Vector3 Target { get; }
    }
}