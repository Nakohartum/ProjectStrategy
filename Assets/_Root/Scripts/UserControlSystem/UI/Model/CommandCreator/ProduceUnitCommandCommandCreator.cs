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
        [Inject] private DiContainer _diContainer;
        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> callback)
        {
            var produceUnitCommand = _context.Inject(new ProduceUnitCommandHeir());
            _diContainer.Inject(produceUnitCommand);
            callback?.Invoke(produceUnitCommand);
        }
    }
}