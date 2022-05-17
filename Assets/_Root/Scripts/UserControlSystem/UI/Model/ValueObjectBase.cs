using System;
using _Root.Scripts.Utils;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem
{
    
    public abstract class ValueObjectBase<T> : ScriptableObject, IAwaitable<T>
    {
        public class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
        {
            private readonly ValueObjectBase<TAwaited> _valueObjectBase;
            private TAwaited _result;
            

            public NewValueNotifier(ValueObjectBase<TAwaited> valueObjectBase)
            {
                _valueObjectBase = valueObjectBase;
                _valueObjectBase.OnNewValue += OnNewValue;
            }

            private void OnNewValue(TAwaited obj)
            {
                _valueObjectBase.OnNewValue -= OnNewValue;
                _result = obj;
                _isCompleted = true;
                _continuation?.Invoke();
            }
            
            public override TAwaited GetResult() => _result;
        }
        public T CurrentValue { get; private set; }
        public Action<T> OnNewValue;

        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifier<T>(this);
        }
    }
}