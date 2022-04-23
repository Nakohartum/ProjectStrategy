using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Injector
{
    [CreateAssetMenu(fileName = nameof(AssetContext), menuName = "Strategy Game/"+nameof(AssetContext), order = 0)]
    public class AssetContext : ScriptableObject
    {
        [SerializeField] private Object[] _objects;
        public Object GetObjectOfType(Type targetType, string targetName = null)
        {
            for (int i = 0; i < _objects.Length; i++)
            {
                var obj = _objects[i];
                if (obj.GetType().IsAssignableFrom(targetType))
                {
                    if (targetName == null || obj.name == targetName)
                    {
                        return obj;
                    }
                }
            }

            return null;
        }
    }
}