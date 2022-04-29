using System;
using _Root.Scripts.Abstractions;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Configs/"+nameof(AttackableValue), order = 0)]
    public class AttackableValue : ValueObjectBase<IAttackable>
    {
        public IAttackable CurrentValue { get; private set; }

        public Action<IAttackable> OnSelected;

        public void SetValue(IAttackable value)
        {
            CurrentValue = value;
            OnSelected?.Invoke(value);
        }
    }
}