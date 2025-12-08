using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

namespace Services.Devices
{
    public class DevicesService : IDevicesService, IInitializable, IDisposable
    {
        public ReactiveCommand<InputDevice> OnDeviceConnected { get; } = new();
        public ReactiveCommand<InputDevice> OnDeviceDisconnected { get; } = new();

        public void Initialize()
        {
            InputDevices.deviceConnected += OnDevicesConnected;
            InputDevices.deviceDisconnected += OnDevicesDisconnected;
            
            InitializeDevices();
        }

        private void InitializeDevices()
        {
            var devices = new List<InputDevice>();
            
            InputDevices.GetDevices(devices);
            
            foreach (var device in devices)
                OnDevicesConnected(device);
        }

        private void OnDevicesConnected(InputDevice device)
        {
            // Debug.Log($"{device.characteristics} connected");
            OnDeviceConnected.Execute(device);
        }

        private void OnDevicesDisconnected(InputDevice device)
        {
            // Debug.Log($"{device.characteristics} disconnected");
            OnDeviceDisconnected?.Execute(device);
        }

        public void Dispose()
        {
            InputDevices.deviceConnected -= OnDevicesConnected;
            InputDevices.deviceDisconnected -= OnDevicesDisconnected;
        }
    }
}