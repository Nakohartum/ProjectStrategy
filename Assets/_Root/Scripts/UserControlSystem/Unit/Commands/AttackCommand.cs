using _Root.Scripts.Abstractions;
using Abstractions;

namespace _Root.Scripts.Core.Unit
{
    public class AttackCommand : IAttackCommand
    {
        public IAttackable Target { get; }

        public AttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}