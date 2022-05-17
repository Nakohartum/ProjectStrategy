using System;

namespace _Root.Scripts.Abstractions
{
    public interface ITimeModel
    {
        IObservable<int> GameTime { get; }
    }
}