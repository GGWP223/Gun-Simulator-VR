using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using UnityEngine.XR;

namespace ECS.System
{
    public class SyncDevicesSystem : IEcsInitSystem
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
                
                if(!component.Devices.Contains(device))
                    component.Devices.Add(device);
            }
        }
        
        private void OnDeviceDisconnected(InputDevice device)
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                
                if(component.Devices.Contains(device))
                    component.Devices.Remove(device);
            }
        }
    }
}