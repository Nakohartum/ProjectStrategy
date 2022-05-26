using System;
using System.Threading;
using _Root.Scripts.Abstractions;
using UniRx;
using UnityEngine;

namespace _Root.Scripts.Core
{
    public class GameStatus : MonoBehaviour, IGameStatus
    {
        private Subject<int> _status = new Subject<int>();
        public IObservable<int> Status => _status;

        private void CheckStatus(object state)
        {
            if (FractionMember.FractionCount == 0)
            {
                _status.OnNext(0);
            }
            else if (FractionMember.FractionCount == 1)
            {
                _status.OnNext(FractionMember.GetWinner());
            }
        }

        private void Update()
        {
            ThreadPool.QueueUserWorkItem(CheckStatus);
        }
    }
}