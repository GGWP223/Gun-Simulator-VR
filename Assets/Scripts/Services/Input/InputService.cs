using System;
using System.Collections.Generic;
using Services.Devices;
using Services.Loop;
using Static;
using UnityEngine;
using UnityEngine.XR;
using UniRx;

namespace Services.Input
{
    public class InputService : IInputService, IEveryUpdate, IDisposable
    {
        public ReactiveProperty<(bool, EHandType)> TriggerState { get; } = new();
        
        public ReactiveProperty<(bool, EHandType)> GripState { get; } = new();
        
        public Vector2 MovementDirection { get; private set; }
        
        public DeviceTransform CameraTransform { get; private set; } = new();

        public DeviceTransform LHandTransform { get; private set; } = new();
        public DeviceTransform RHandTransform { get; private set; } = new();

        private readonly IDevicesService _device;
        
        private readonly List<InputDevice> _devices = new();
        private readonly CompositeDisposable _disposables = new();
        
        public InputService
        (
            IDevicesService device
        )
        {
            _device = device;
            
            Subscribe();
        }

        public void Update()
        {
            foreach (var device in _devices)
            {
                var chars = device.characteristics;

                if (chars.HasFlag(InputDeviceCharacteristics.HeadMounted))
                {
                    UpdateTransform(device, CameraTransform);
                    continue;
                }

                if (chars.HasFlag(InputDeviceCharacteristics.Controller))
                    UpdateController(chars, device);
            }
        }

        private void Subscribe()
        {
            _device.OnDeviceConnected
                .Subscribe(device => _devices.Add(device))
                .AddTo(_disposables);
        }

        private void UpdateController(InputDeviceCharacteristics chars, InputDevice device)
        {
            GripState.Value = GetButtonState(device, CommonUsages.gripButton);
            TriggerState.Value = GetButtonState(device, CommonUsages.triggerButton);
            
            if (chars.HasFlag(InputDeviceCharacteristics.Left))
            {
                UpdateTransform(device, LHandTransform);
                return;
            }

            if (!chars.HasFlag(InputDeviceCharacteristics.Right)) 
                return;
            
            UpdateTransform(device, RHandTransform);
            MovementDirection = GetJoystickAxis(device);
        }

        private void UpdateTransform(InputDevice device, DeviceTransform transform)
        {
            if(!device.isValid)
                return;
            
            if(device.TryGetFeatureValue(CommonUsages.devicePosition, out var position))
                transform.Position = position;
            
            if(device.TryGetFeatureValue(CommonUsages.deviceRotation, out var rotation))
                transform.Rotation = rotation;
        }

        private Vector2 GetJoystickAxis(InputDevice device)
        {
            return device.TryGetFeatureValue(CommonUsages.primary2DAxis, out var value) ? value : Vector2.zero;
        }
        
        private (bool, EHandType) GetButtonState(InputDevice device, InputFeatureUsage<bool> button)
        {
            var type = (device.characteristics & InputDeviceCharacteristics.Left) != 0 ? EHandType.Left : EHandType.Right;

            return device.TryGetFeatureValue(button, out bool value)
                ? (value, type)
                : (false, EHandType.None);
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}