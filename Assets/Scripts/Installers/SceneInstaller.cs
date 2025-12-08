using Services.Camera;
using Services.Character.Controller;
using Services.Character.Hands;
using Services.Character.Hands.Grab;
using Services.Character.Hands.Rigging;
using Services.Character.Motion;
using Services.Devices;
using Services.Input;
using Services.Loop;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Application.targetFrameRate = 120;
        
        Bind<DevicesService>();
        Bind<InputService>();

        Bind<HandsMotionService>();
        Bind<HandsRiggingService>();
        Bind<HandsGrabService>();
        
        Bind<PlayerCameraService>();
        Bind<PlayerMotionService>();
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
