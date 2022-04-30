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

public class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable, IAttackable
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform _unitsParent;
    
    [field: Header("Building Stats")]
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public int MilliSecondsToSpawn { get; private set; }
    [field: SerializeField] public Transform PivotPoint { get; private set; }
    [field: Header("Select Icon")]
    [field: SerializeField] public Sprite Icon { get; private set; }
    

    public override async void ExecuteSpecificCommand(IProduceUnitCommand command)
    {
        var deltaTime = Time.deltaTime;
        await Task.Run(() => WaitForTime(deltaTime));
        Instantiate(command.UnitPrefab, new Vector3(Random.Range(-10, 11), 0, Random.Range(-10, 11)),
            Quaternion.identity, _unitsParent);
    }

    private void WaitForTime(float deltaTime)
    {
        float time = 0.0f;
        while (time <= 2f)
        {
            time += deltaTime;
            Thread.Sleep((int)(deltaTime*1000));
        }
    }
    
}
