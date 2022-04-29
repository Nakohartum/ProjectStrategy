using System;
using Abstractions;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(SelectableObject), menuName = "Config/"+nameof(SelectableObject))]

    public class SelectableObject : ValueObjectBase<ISelectable>
    {
        public ISelectable CurrentValue { get; private set; }

        public Action<ISelectable> OnSelected;

        public void SetValue(ISelectable value)
        {
            CurrentValue = value;
            OnSelected?.Invoke(value);
        }
    }
}