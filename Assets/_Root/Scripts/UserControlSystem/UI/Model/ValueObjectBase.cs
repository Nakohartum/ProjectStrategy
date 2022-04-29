using System;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem
{
    
    public abstract class ValueObjectBase<T> : ScriptableObject
    {
        public T CurrentValue { get; private set; }
        public Action<T> OnNewValue;

        public void SetValue(T value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }
    }
}