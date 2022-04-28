using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core.Unit
{
    public class MainUnit : MonoBehaviour, ISelectable
    {
        [field: Header("Unit characteristics")]
        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: SerializeField] public float MaxHealth { get; private set; }
        public float Health { get; private set; } = 100;
    }
}