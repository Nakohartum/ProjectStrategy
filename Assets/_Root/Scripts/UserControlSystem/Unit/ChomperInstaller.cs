using _Root.Scripts.Abstractions;
using Zenject;

namespace _Root.Scripts.Core.Unit
{
    public class ChomperInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var health = gameObject.GetComponent<IHealth>();
            Container.Bind<IHealth>().FromInstance(health);
            Container.Bind<float>().WithId("AttackDistance").FromInstance(5f);
            Container.Bind<float>().WithId("AttackPeriod").FromInstance(1400);
        }
    }
}