using System;
using Abstractions;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(SelectableObject), menuName = "Config/"+nameof(SelectableObject))]

    public class SelectableObject : StatefulValueObjectBase<ISelectable>
    {
        
    }
}