using Components;
using ECS.Reference;
using ECS.Tag;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.XR;

namespace ECS.System
{
    public class HandTransformSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RightHandTag, TransformReference, ConfigurableJointReference> _rightFilter = null;
        private readonly EcsFilter<LeftHandTag, TransformReference, ConfigurableJointReference> _leftFilter = null;
        
        private readonly EcsFilter<DevicesComponent> _devicesFilter = null;
        
        public void Run()
        {
            foreach (var entity in _devicesFilter)
            {
                var devices = _devicesFilter.Get1(entity);
                
                ref var rightTransform = ref _rightFilter.Get2(0);
                ref var rightJoint = ref _rightFilter.Get3(0);
                
                ref var leftTransform = ref _leftFilter.Get2(0);
                ref var leftJoint = ref _leftFilter.Get3(0);

                foreach (var device in devices.Devices)
                {
                    if ((device.characteristics & InputDeviceCharacteristics.Controller) == 0)
                        continue;

                    if ((device.characteristics & InputDeviceCharacteristics.Right) != 0)
                        UpdateInput(ref rightTransform, ref rightJoint, device);

                    if ((device.characteristics & InputDeviceCharacteristics.Left) != 0)
                        UpdateInput(ref leftTransform, ref leftJoint, device);
                }
            }
        }

        private void UpdateInput(ref TransformReference transformReference, ref ConfigurableJointReference jointReference, InputDevice device)
        {
            var transform = transformReference.Transform;
            var joint = jointReference.Joint;
            
            device.TryGetFeatureValue(CommonUsages.devicePosition, out var position);
            device.TryGetFeatureValue(CommonUsages.deviceRotation, out var rotation);
                
            transform.localPosition = position;
            transform.localRotation = rotation;

            joint.targetPosition = transform.localPosition;
            joint.targetRotation = transform.localRotation;
        }
    }
}