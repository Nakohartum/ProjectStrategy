using _Root.Scripts.Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core
{
    public class UnitProductionTask : IUnitProductionTask
    {
        public Sprite Icon { get; }
        public string UnitName { get; }
        public float TimeLeft { get; set; }
        public float ProductionTime { get; }
        public GameObject UnitPrefab { get; }

        public UnitProductionTask(Sprite icon, string unitName, float productionTime, GameObject unitPrefab)
        {
            Icon = icon;
            UnitName = unitName;
            TimeLeft = productionTime;
            ProductionTime = productionTime;
            UnitPrefab = unitPrefab;
        }
    }
}