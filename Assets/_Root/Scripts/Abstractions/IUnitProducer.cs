using UniRx;

namespace _Root.Scripts.Abstractions
{
    public interface IUnitProducer
    {
        IReadOnlyReactiveCollection<IUnitProductionTask> Queue { get; }
        public void Cancel(int index);
    }
}