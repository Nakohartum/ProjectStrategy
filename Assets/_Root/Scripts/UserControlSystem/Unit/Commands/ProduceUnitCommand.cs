using Abstractions;
using Injector;
using UnityEngine;
using Zenject;

namespace UserControl
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [InjectAsset("Chomper")]private GameObject _unitPrefab;
        public GameObject UnitPrefab => _unitPrefab;
        [Inject(Id = "Chomper")]public float ProductionTime { get; }
        [Inject(Id = "Chomper")]public Sprite Icon { get; }
        [Inject(Id = "Chomper")]public string UnitName { get; }
    }
}