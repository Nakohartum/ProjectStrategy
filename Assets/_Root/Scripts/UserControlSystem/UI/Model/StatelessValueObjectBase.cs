using System;
using UniRx;

namespace _Root.Scripts.UserControlSystem
{
    public class StatelessValueObjectBase<T> : ValueObjectBase<T>, IObservable<T>
    {
        private Subject<T> _data = new Subject<T>();

        public override void SetValue(T value)
        {
            base.SetValue(value);
            _data.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _data.Subscribe(observer);
        }
    }
}