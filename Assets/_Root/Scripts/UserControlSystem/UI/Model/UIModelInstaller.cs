using _Root.Scripts.UserControlSystem.CommandCreator;
using Abstractions;
using Injector;
using UnityEngine;
using Zenject;

namespace _Root.Scripts.UserControlSystem
{
    public class UIModelInstaller : MonoInstaller
    {
        [SerializeField] private AssetContext _legacyContext;
        [SerializeField] private Vector3Value _vector3Value;

        public override void InstallBindings()
        {
            Container.Bind<AssetContext>().FromInstance(_legacyContext);
            Container.Bind<Vector3Value>().FromInstance(_vector3Value);
            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCommandCreator>()
                .AsTransient();
            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCommandCreator>()
                .AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCommandCreator>()
                .AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCommandCreator>()
                .AsTransient();
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCommandCreator>()
                .AsTransient();

            Container.Bind<CommandsButtonModel>().AsTransient();
        }
    }
}