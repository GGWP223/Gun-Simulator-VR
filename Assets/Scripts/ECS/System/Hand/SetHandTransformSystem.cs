using Components;
using ECS.Tag;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.XR;

namespace ECS.System
{
    public class SetHandTransformSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RightHandTag, TransformComponent> _rightFilter = null;
        private readonly EcsFilter<LeftHandTag, TransformComponent> _leftFilter = null;
        
        private readonly EcsFilter<DevicesComponent> _devicesFilter = null;
        
        public void Run()
        {
            foreach (var entity in _devicesFilter)
            {
                var devices = _devicesFilter.Get1(entity);
                
                ref var right = ref _rightFilter.Get2(0);
                ref var left = ref _leftFilter.Get2(0);
                
                UpdateInput(ref right, devices, InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller);
                UpdateInput(ref left, devices, InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller);
            }
        }

        private void UpdateInput(ref TransformComponent transform, DevicesComponent devices, InputDeviceCharacteristics characteristics)
        {
            foreach (var device in devices.Devices)
            {
                if ((device.characteristics & characteristics) == 0)
                    continue;
                    
                transform.Position = device.TryGetFeatureValue(CommonUsages.devicePosition, out var position) ? position : Vector3.zero;
                transform.Rotation = device.TryGetFeatureValue(CommonUsages.deviceRotation, out var rotation) ? rotation : Quaternion.identity;
                    
                break;
            }
        }
    }
}