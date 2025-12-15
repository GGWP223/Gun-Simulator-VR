using DI.Service.Player.Create;
using Zenject;

namespace DI.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Bind<PlayerInitializeService>();
        }

        public override void Start()
        {
            Container.Resolve<IPlayerInitializeService>().Initialize();
        }
        
        private void Bind<T>() where T : class
        {
            Container
                .BindInterfacesTo<T>()
                .AsSingle();
        }
    }
}