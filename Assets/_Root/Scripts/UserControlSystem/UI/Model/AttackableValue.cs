using System;
using _Root.Scripts.Abstractions;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Configs/"+nameof(AttackableValue), order = 0)]
    public class AttackableValue : StatelessValueObjectBase<IAttackable>
    {
        
    }
}