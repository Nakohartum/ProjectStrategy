using _Root.Scripts.Abstractions;
using UnityEngine;

namespace Abstractions
{
    public interface IProduceUnitCommand : ICommand, IIconHolder
    {
        GameObject UnitPrefab { get; }
        float ProductionTime { get; }
        Sprite Icon { get; }
        string UnitName { get; }
    }
}