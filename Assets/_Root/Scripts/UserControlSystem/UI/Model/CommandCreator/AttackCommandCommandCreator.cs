using System;
using _Root.Scripts.Abstractions;
using _Root.Scripts.Core.Unit;
using Abstractions;
using Injector;
using Zenject;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetContext _context;
        private Action<IAttackCommand> _creationCallback;

        [Inject]
        private void Init(AttackableValue attackableValue)
        {
            attackableValue.OnSelected += OnNewValue;
        }

        private void OnNewValue(IAttackable attackable)
        {
            _creationCallback?.Invoke(_context.Inject(new AttackCommand(attackable)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> callback)
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