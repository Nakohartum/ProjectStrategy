using System;
using _Root.Scripts.Utils;

namespace _Root.Scripts.UserControlSystem
{
    public abstract class AwaiterBase<T> : IAwaiter<T>
    {
        protected Action _continuation;
        protected bool _isCompleted;
        private T _result;
        public bool IsCompleted => _isCompleted;
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

        protected void OnWaitFinish(T result)
        {
            _result = result;
            _isCompleted = true;
            _continuation?.Invoke();
        }
        
        public T GetResult() => _result;
    }
}