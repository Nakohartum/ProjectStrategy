using _Root.Scripts.Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core
{
    public class FractionMember : MonoBehaviour, IFractionMember
    {
        [field: SerializeField]public int FractionID { get; set; }
    }
}