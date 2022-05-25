using _Root.Scripts.Abstractions;
using _Root.Scripts.UserControlSystem.Unit.Commands;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class SetRallyPointCommandCommandCreator : CancellableCommandCreatorBase<ISetRallyPointCommand, Vector3>
    {
        protected override ISetRallyPointCommand CreateCommand(Vector3 argument)
        {
            return new SetRallyPoint(argument);
        }
    }
}