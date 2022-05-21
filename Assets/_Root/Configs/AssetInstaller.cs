using System;
using _Root.Scripts.Abstractions;
using _Root.Scripts.UserControlSystem;
using _Root.Scripts.Utils;
using Abstractions;
using Injector;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AssetInstaller", menuName = "Installers/AssetInstaller")]
public class AssetInstaller : ScriptableObjectInstaller<AssetInstaller>
{
    [SerializeField] private AssetContext _legacyContext;
    [SerializeField] private Vector3Value _vector3Value;
    [SerializeField] private AttackableValue _attackableValue;
    [SerializeField] private SelectableObject _selectableObject;
    [SerializeField] private Sprite _chomperSprite;

    public override void InstallBindings()
    {
        Container.BindInstances(_legacyContext, _vector3Value, _attackableValue, _selectableObject);
        Container.Bind<IAwaitable<IAttackable>>().FromInstance(_attackableValue);
        Container.Bind<IAwaitable<Vector3>>().FromInstance(_vector3Value);
        Container.Bind<IObservable<ISelectable>>().FromInstance(_selectableObject);
        Container.Bind<Sprite>().WithId("Chomper").FromInstance(_chomperSprite);
    }
}