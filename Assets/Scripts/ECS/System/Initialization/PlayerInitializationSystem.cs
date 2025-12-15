using Components;
using ECS.Reference;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.System.Initialization
{
    public class PlayerInitializationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private PlayerInitializationRequest _request;

        public void SendRequest(PlayerInitializationRequest request)
        {
            _request = request;
        }
        
        public void Run()
        {
            if(_request is null)
                return;
            
            var entity = _world.NewEntity();

            ref var characterReference = ref entity.Get<CharacterReference>();
            ref var transformReference = ref entity.Get<TransformReference>();
            
            ref var characterComponent = ref entity.Get<CharacterComponent>();
            ref var transformComponent = ref entity.Get<TransformComponent>();
            ref var inputComponent = ref entity.Get<PlayerInputComponent>();
            
            characterReference.Controller = _request.Controller;
            transformReference.Transform = _request.Transform;

            _request = null;
        }
    }

    public class PlayerInitializationRequest
    {
        public Transform Transform { get; }
        public CharacterController Controller { get; }

        public PlayerInitializationRequest(CharacterController controller, Transform transform)
        {
            Controller = controller;
            Transform = transform;
        }
    }
}