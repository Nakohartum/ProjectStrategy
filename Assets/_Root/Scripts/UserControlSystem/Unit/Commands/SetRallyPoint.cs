using _Root.Scripts.Abstractions;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem.Unit.Commands
{
    public class SetRallyPoint : ISetRallyPointCommand
    {
        public Vector3 RallyPoint { get; }

        public SetRallyPoint(Vector3 rallyPoint)
        {
            RallyPoint = rallyPoint;
        }
    }
}