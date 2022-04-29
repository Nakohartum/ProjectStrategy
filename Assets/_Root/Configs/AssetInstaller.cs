using _Root.Scripts.UserControlSystem;
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
    public override void InstallBindings()
    {
        Container.BindInstances(_legacyContext, _vector3Value, _attackableValue, _selectableObject);
    }
}