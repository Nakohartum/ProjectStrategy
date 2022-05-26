using System.Threading.Tasks;
using _Root.Scripts.Abstractions;
using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core.Unit
{
    public class MainUnit : MonoBehaviour,ISelectable, IUnit, IAttackable, IDamageDealer
    {
        [field: Header("Unit characteristics")]
        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: SerializeField] public int Damage { get; private set; } = 25;
        [field: SerializeField] public Transform PivotPoint { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommand;
        private int PlayDeadHash = Animator.StringToHash("PlayDead");
        public float Health { get; private set; } = 100;
        
        public void RecieveDamage(int damage)
        {
            if (Health <= 0)
            {
                return;
            }

            Health -= damage;
            if (Health <= 0)
            {
                _animator.SetTrigger(PlayDeadHash);
                Kill();
            }
        }

        private async void Kill()
        {
            await Task.Delay(1000);
            await _stopCommand.ExecuteSpecificCommand(new StopCommand());
            Destroy(gameObject);
        }

        
    }
}