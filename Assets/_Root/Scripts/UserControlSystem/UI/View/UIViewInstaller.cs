using Zenject;

namespace UI.View
{
    public class UIViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BottomCenterView>().FromComponentInHierarchy().AsSingle();
        }
    }
}