using System;
using UniRx;

namespace _Root.Scripts.Utils
{
    public static class Extensions
    {
        public static IDisposable Subscribe<T1>(this IObservable<CollectionAddEvent<T1>> source, Action<T1, int> OnNext)
        {
            return ObservableExtensions.Subscribe(source, t => OnNext(t.Value, t.Index));
        }
    }
}