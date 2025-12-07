using System;
using Services.Camera;
using Services.Character.Hands;
using Services.Devices;
using Services.Loop;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

namespace Services.Character.Controller
{
    public class PlayerControllerService : IPlayerControllerService, IEveryUpdate, IDisposable
    {
        private readonly IDevicesService _devices;
        private readonly IPlayerCameraService _camera;
        private readonly IPlayerHandsService _hands;

        private readonly CompositeDisposable _disposable = new();

        public PlayerControllerService
        (
            IDevicesService devices,
            IPlayerCameraService camera,
            IPlayerHandsService hands
        )
        {
            _devices = devices;
            _camera = camera;
            _hands = hands;

            Subscribe();
        }

        private void Subscribe()
        {
            _devices.OnDeviceConnected
                .Subscribe(device =>
                {
                    _camera.SetDevice(device);
                    _hands.SetDevice(device);
                })
                .AddTo(_disposable);
        }

        public void Update()
        {
            _camera.UpdateTransform();
            _hands.UpdateTransform();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}