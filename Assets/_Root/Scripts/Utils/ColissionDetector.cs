using System;
using UniRx;
using UnityEngine;

namespace _Root.Scripts.Utils
{
    public class ColissionDetector : MonoBehaviour
    {
        private Subject<Collision> _collisions = new Subject<Collision>();
        public IObservable<Collision> Collisions => _collisions;

        private void OnCollisionStay(Collision other)
        {
            _collisions.OnNext(other);
        }
    }
}