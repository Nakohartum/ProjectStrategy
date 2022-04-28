using System;
using Abstractions;
using Injector;
using UserControl;
using Zenject;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public sealed class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private AssetContext _context;
        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> callback)
        {
            callback?.Invoke(_context.Inject(new ProduceUnitCommandHeir()));
        }
    }
}