using System;
using _Root.Scripts.Abstractions;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Root.Scripts.UserControlSystem
{
    public class BottomCenterModel
    {
        public IObservable<IUnitProducer> UnitProducers { get; private set; }

        [Inject]
        private void Init(IObservable<ISelectable> currentlySelected)
        {
            UnitProducers = currentlySelected.Select(selectable => selectable as Component)
                .Select(component => component?.GetComponent<IUnitProducer>());
        }
    }
}