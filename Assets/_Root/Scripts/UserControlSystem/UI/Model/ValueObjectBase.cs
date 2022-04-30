using System;
using _Root.Scripts.Utils;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem
{
    
    public abstract class ValueObjectBase<T> : ScriptableObject, IAwaitable<T>
    {
        public class NewValueNotifier<TAwaited> : IAwaiter<TAwaited>
        {
            private readonly ValueObjectBase<TAwaited> _valueObjectBase;
            private TAwaited _result;
            private Action _continuation;
            private bool _isCompleted;
            
            public bool IsCompleted => _isCompleted;

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

            public void OnCompleted(Action continuation)
            {
                if (_isCompleted)
                {
                    continuation?.Invoke();
                }
                else
                {
                    _continuation = continuation;
                }
            }

            
            public TAwaited GetResult() => _result;
        }
        public T CurrentValue { get; private set; }
        public Action<T> OnNewValue;

        public void SetValue(T value)
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