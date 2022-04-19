using UnityEngine;
using Outlines;
namespace Abstractions
{
    public interface ISelectable
    {
        float MaxHealth { get; }
        float Health { get; }
        Sprite Icon { get; }
        Outline Outline { get; }
    }
}