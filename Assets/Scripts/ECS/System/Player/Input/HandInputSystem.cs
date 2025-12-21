using Components;
using ECS.Tag;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.XR;

namespace ECS.System
{
    public class HandInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RightHandTag, HandInputComponent> _rightFilter = null;
        private readonly EcsFilter<LeftHandTag, HandInputComponent> _leftFilter = null;
        
        private readonly EcsFilter<DevicesComponent> _devices = null;
        
        public void Run()
        {
            foreach (var entity in _devices)
            {
                var devices = _devices.Get1(entity);
                
                ref var right = ref _rightFilter.Get2(0);
                ref var left = ref _leftFilter.Get2(0);
                
                UpdateInput(ref right, devices, InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller);
                UpdateInput(ref left, devices, InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller);
            }
        }

        private void UpdateInput(ref HandInputComponent input, DevicesComponent devices, InputDeviceCharacteristics characteristics)
        {
            foreach (var device in devices.Devices)
            {
                if ((device.characteristics & characteristics) == 0)
                    continue;
                    
                input.Grip = device.TryGetFeatureValue(CommonUsages.grip, out var grip) ? grip : 0;
                input.Trigger = device.TryGetFeatureValue(CommonUsages.trigger, out var trigger) ? trigger : 0;
                input.Joystick = device.TryGetFeatureValue(CommonUsages.primary2DAxis, out var joystick) ? joystick : Vector2.zero;
                    
                break;
            }
        }
    }
}