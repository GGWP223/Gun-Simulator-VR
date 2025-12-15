using System;
using ECS.System;
using ECS.System.Initialization;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    public class SystemInstaller : MonoInstaller
    {
        private EcsWorld _world;
        
        private EcsSystems _updates;
        
        private bool _isInitialized;
        
        public override void InstallBindings()
        {
            _world = new EcsWorld();
            _updates = new EcsSystems(_world);
            
            AddSystems();
            
            _updates.Init();
            
            _isInitialized = true;
        }
        
        public void Update()
        {
            if(!_isInitialized)
                return;
            
            _updates.Run();
        }

        private void AddSystems()
        {
            _updates
                .Add(Bind<SyncTransformSystem>())
                .Add(Bind<SyncDevicesSystem>())
                .Add(Bind<SyncCharacterSystem>())
                .Add(Bind<SetHandInputSystem>())
                .Add(Bind<SetHandTransformSystem>())
                .Add(Bind<SetHeadTransformSystem>())
                .Add(Bind<SetPlayerInputSystem>())
                .Add(Bind<SetPlayerDirectionSystem>())
                .Add(Bind<PlayerInitializationSystem>());
        }
        
        private void OnDestroy()
        {
            if(_updates is null)
                return;
            
            _updates.Destroy();
            _world.Destroy();
            
            _world = null;
            _updates = null;
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