using _Root.Scripts.Abstractions;
using UnityEngine;
namespace Abstractions
{
    public interface ISelectable : IHealth, IIconHolder
    {
        Transform PivotPoint { get; }
       
    }
}