using System;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Configs/"+nameof(Vector3Value), order = 0)]
    public class Vector3Value : ValueObjectBase<Vector3>
    {
       
    }
}