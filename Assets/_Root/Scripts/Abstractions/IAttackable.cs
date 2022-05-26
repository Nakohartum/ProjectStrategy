namespace _Root.Scripts.Abstractions
{
    public interface IAttackable : IHealth
    {
        void RecieveDamage(int damage);
    }
}