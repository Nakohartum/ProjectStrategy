using System;
using _Root.Scripts.Core.Unit;
using Abstractions;
using Injector;
using UnityEngine;
using Zenject;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class MoveCommandCommandCreator : CommandCreatorBase<IMoveCommand>
    {
        [Inject] private AssetContext _context;
        private Action<IMoveCommand> _creationCallback;

        [Inject]
        private void Init(Vector3Value groundClicks) => groundClicks.OnNewValue += OnNewValue;

        private void OnNewValue(Vector3 groundClicks)
        {
            _creationCallback?.Invoke(_context.Inject(new MoveCommand(groundClicks)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IMoveCommand> callback)
        {
            _creationCallback = callback;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _creationCallback = null;
        }
    }
}