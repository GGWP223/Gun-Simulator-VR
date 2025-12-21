using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.XR;

namespace ECS.System
{
    public class SyncDeviceSystem : IEcsInitSystem
    {
        private readonly EcsFilter<DevicesComponent> _filter = null;
        
        public void Init()
        {
            foreach (var device in _filter)
            {
                ref var component = ref _filter.Get1(device);

                component.Devices = new List<InputDevice>();

                InputDevices.deviceConnected += OnDeviceConnected;
                InputDevices.deviceDisconnected += OnDeviceDisconnected;
                
                InputDevices.GetDevices(component.Devices);
            }
        }

        private void OnDeviceConnected(InputDevice device)
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                
                Debug.Log($"Try add {device.name}");
                
                component.Devices.Add(device);
            }
        }
        
        private void OnDeviceDisconnected(InputDevice device)
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                
                Debug.Log($"Try remove {device.name}");

                component.Devices.Remove(device);
            }
        }
    }
}