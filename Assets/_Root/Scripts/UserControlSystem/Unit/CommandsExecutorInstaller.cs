using Abstractions;
using Zenject;

namespace _Root.Scripts.Core.Unit
{
    public class CommandsExecutorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var executors = gameObject.GetComponentsInChildren<ICommandExecutor>();
            for (int i = 0; i < executors.Length; i++)
            {
                var baseType = executors[i].GetType().BaseType;
                Container.Bind(baseType).FromInstance(executors[i]);
            }
        }
    }
}