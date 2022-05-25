using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Abstractions
{
    public interface ISetRallyPointCommand : ICommand
    {
        Vector3 RallyPoint { get; }
    }
}