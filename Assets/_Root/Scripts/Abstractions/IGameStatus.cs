using System;

namespace _Root.Scripts.Abstractions
{
    public interface IGameStatus
    {
        IObservable<int> Status { get; }
    }
}