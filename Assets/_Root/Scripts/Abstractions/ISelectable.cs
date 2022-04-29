using _Root.Scripts.Abstractions;
using UnityEngine;
namespace Abstractions
{
    public interface ISelectable : IHealth
    {
        Transform PivotPoint { get; }
        Sprite Icon { get; }
    }
}