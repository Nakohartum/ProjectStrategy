using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _Root.Scripts.Abstractions;
using Abstractions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainBuilding : MonoBehaviour, ISelectable, IAttackable, IHealth
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform _unitsParent;

    public Vector3 RallyPoint { get; set; }
    
    [field: Header("Building Stats")]
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public int MilliSecondsToSpawn { get; private set; }
    [field: SerializeField] public Transform PivotPoint { get; private set; }
    [field: Header("Select Icon")]
    [field: SerializeField] public Sprite Icon { get; private set; }

    public void RecieveDamage(int damage)
    {
        if (Health <= 0)
        {
            return;
        }

        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
