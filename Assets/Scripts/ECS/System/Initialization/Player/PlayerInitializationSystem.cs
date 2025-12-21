using System.ComponentModel;
using Components;
using DI.View;
using ECS.Reference;
using ECS.Tag;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.System.Initialization
{
    public class PlayerInitializationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private PlayerInitializationRequest _player;

        public void SendRequest(PlayerInitializationRequest player)
        {
            _player = player;
        }
        
        public void Run()
        {
            if(_player is null)
                return;
            
            InitializeBody();
            
            InitializeHand<RightHandTag>(_player.Hand.RHand);
            InitializeHand<LeftHandTag>(_player.Hand.LHand);
            
            InitializeRigging();

            _player = null;
        }

        private void InitializeBody()
        {
            var body = _world.NewEntity();
            var head = _world.NewEntity();
            
            body.Get<BodyTag>();
            body.Get<RigidbodyReference>();
            body.Get<ControllerInputComponent>();
            body.Get<ColliderReference>().Collider = _player.Player.Collider;

            head.Get<HeadTag>();
            head.Get<TransformReference>().Transform = _player.Head.Camera.transform;
        }

        private void InitializeRigging()
        {
            var entity = _world.NewEntity();
            var rigging = _player.Rigging;
            
            ref var component = ref entity.Get<ControllerRiggingComponent>();

            entity.Get<ControllerTag>();
            
            component.BodyRig = rigging.BodyRig;
            component.HeadRig = rigging.HeadRig;
            component.LeftArmRig = rigging.LeftArmRig;
            component.RightArmRig = rigging.RightArmRig;
            component.LeftLegRig = rigging.LeftLegRig;
            component.RightLegRig = rigging.RightLegRig;
        }

        private void InitializeHand<T>(Hand hand) where T : struct
        {
            var entity = _world.NewEntity();
            
            entity.Get<T>();
            
            entity.Get<ConfigurableJointReference>().Joint = hand.Joint;
            entity.Get<TransformReference>().Transform = hand.Transform;
        }
    }
}