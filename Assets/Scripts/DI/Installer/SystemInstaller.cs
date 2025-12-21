using System;
using ECS.System;
using ECS.System.Initialization;
using ECS.System.Initialization.Scene;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    public class SystemInstaller : MonoInstaller
    {
        private EcsWorld _world;
        
        private EcsSystems _updates;
        private EcsSystems _fixedUpdates;
        
        private bool _isInitialized;
        
        public override void InstallBindings()
        {
            _world = new EcsWorld();
            
            _updates = new EcsSystems(_world);
            _fixedUpdates = new EcsSystems(_world);
            
            AddSystems();
            
            _updates.Init();
            _fixedUpdates.Init();
            
            _isInitialized = true;
        }
        
        private void Update()
        {
            if(!_isInitialized)
                return;
            
            _updates.Run();
        }

        private void FixedUpdate()
        {
            if(!_isInitialized)
                return;
            
            _fixedUpdates.Run();
        }

        private void AddSystems()
        {
            _updates
                .Add(Bind<SceneInitializationSystem>())
                .Add(Bind<PlayerInitializationSystem>())

                .Add(Bind<SyncDeviceSystem>())
                
                .Add(Bind<BodyTransformSystem>())

                .Add(Bind<HandInputSystem>())
                .Add(Bind<HandTransformSystem>())

                .Add(Bind<HeadTransformSystem>())

                .Add(Bind<ControllerInputSystem>())
                // .Add(Bind<ControllerMotionSystem>())
                .Add(Bind<ControllerRiggingSystem>());
;        }
        
        private void OnDestroy()
        {
            if(_updates is null)
                return;
            
            _updates.Destroy();
            _world.Destroy();
            
            _world = null;
            _updates = null;
            _fixedUpdates = null;
        }
        
        private T Bind<T>() where T : class
        {
            Container
                .Bind<T>()
                .AsSingle();
            
            return Container.Resolve<T>();
        }
    }
}