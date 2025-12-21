using System;
using Components;
using ECS.Reference;
using ECS.Tag;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.System
{
    public class ControllerRiggingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ControllerTag, ControllerRiggingComponent> _rigFilter;
        
        private readonly EcsFilter<RightHandTag, TransformReference> _rightHandFilter;
        private readonly EcsFilter<LeftHandTag, TransformReference> _leftHandFilter;
        
        private readonly EcsFilter<BodyTag, ColliderReference> _bodyFilter;
        private readonly EcsFilter<HeadTag, TransformReference> _headFilter;
        
        public void Run()
        {
            foreach (var entity in _rigFilter)
            {
                var rig = _rigFilter.Get2(entity);

                foreach (var i in _rightHandFilter)
                {
                    var reference = _rightHandFilter.Get2(i);
                    var arm = rig.RightArmRig;
                    
                    arm.Target.position = reference.Transform.position;
                    arm.Target.localRotation = reference.Transform.localRotation * Quaternion.Euler(arm.RotationOffset);
                }
                
                foreach (var i in _leftHandFilter)
                {
                    var reference = _leftHandFilter.Get2(i);
                    var arm = rig.LeftArmRig;
                    
                    arm.Target.position = reference.Transform.position;
                    arm.Target.localRotation = reference.Transform.localRotation * Quaternion.Euler(arm.RotationOffset);
                }

                foreach (var i in _bodyFilter)
                {
                    var reference = _bodyFilter.Get2(i);
                    var body = rig.BodyRig;
                    
                    if (reference.Collider is not CapsuleCollider collider)
                        throw new Exception($"{reference.Collider.name} is not a capsule collider.");
                    
                    body.Target.localPosition = new Vector3(
                        collider.center.x,
                        collider.height / 2, 
                        collider.center.z
                    );
                }

                foreach (var i in _headFilter)
                {
                    var reference = _headFilter.Get2(i);
                    var head = rig.HeadRig;
                    
                    head.Target.localRotation = reference.Transform.localRotation * Quaternion.Euler(head.RotationOffset);
                }
            }
        }
    }
}