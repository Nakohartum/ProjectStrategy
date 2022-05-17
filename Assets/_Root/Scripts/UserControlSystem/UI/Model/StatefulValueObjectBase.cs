using System;
using UniRx;

namespace _Root.Scripts.UserControlSystem
{
    public abstract class StatefulValueObjectBase<T> : ValueObjectBase<T>, IObservable<T>
    {
        private ReactiveProperty<T> _data = new ReactiveProperty<T>();
        public override void SetValue(T value)
        {
            base.SetValue(value);
            _data.Value = value;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _data.Subscribe(observer);
        }
    }
}