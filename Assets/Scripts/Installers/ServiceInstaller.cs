using Services.Camera;
using Services.Character.Controller;
using Services.Character.Hands;
using Services.Devices;
using Services.Loop;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Bind<DevicesService>();
        
        Bind<PlayerCameraService>();
        Bind<PlayerHandsService>();
        Bind<PlayerControllerService>();
        
        Bind<LoopService>();
    }

    private void Bind<T>() where T : class
    {
        Container
            .BindInterfacesTo<T>()
            .AsSingle();
    }
}
