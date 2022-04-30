using System;
using System.Threading;
using _Root.Scripts.Utils;
using Abstractions;
using Injector;
using Zenject;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public abstract class CancellableCommandCreatorBase<TCommand, TArgument> : CommandCreatorBase<TCommand> where TCommand : ICommand
    {
        [Inject] private AssetContext _context;
        [Inject] private IAwaitable<TArgument> _awaitableArgument;

        private CancellationTokenSource _ctSource;

        protected override async void ClassSpecificCommandCreation(Action<TCommand> callback)
        {
            _ctSource = new CancellationTokenSource();
            try
            {
                var argument = await _awaitableArgument.WithCancellation(_ctSource.Token);
                callback?.Invoke(_context.Inject(CreateCommand(argument)));
            }
            catch 
            {
            }
        }

        protected abstract TCommand CreateCommand(TArgument argument);

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            if (_ctSource != null)
            {
                _ctSource.Cancel();
                _ctSource.Dispose();
                _ctSource = null;
            }
        }
    }
}