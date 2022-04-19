using System.Collections;
using System.Collections.Generic;
using Abstractions;
using Outlines;
using UnityEngine;

public class MainBuilding : MonoBehaviour, IUnitProduce, ISelectable
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private Transform _unitsParent;
    
    [field: Header("Building Stats")]
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    
    [field: Header("Select Icon")]
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public Outline Outline { get; private set; }

    public void ProduceUnit()
    {
        var spawnPosition = new Vector3(Random.Range(-10, 11), 0, Random.Range(-10, 11));
        Instantiate(_unitPrefab, spawnPosition, Quaternion.identity, _unitsParent);
    }

    
}
