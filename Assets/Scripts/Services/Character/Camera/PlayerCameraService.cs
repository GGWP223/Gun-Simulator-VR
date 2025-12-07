using UnityEngine;
using UnityEngine.XR;
using View;
using Zenject;

namespace Services.Camera
{
    public class PlayerCameraService : IPlayerCameraService
    {
        private readonly CameraView _view;
        
        private InputDevice _device;

        public PlayerCameraService
        (
            CameraView view
        )
        {
            _view = view;
        }

        public void SetDevice(InputDevice device)
        {
            if((device.characteristics & InputDeviceCharacteristics.HeadMounted) != 0)
                _device = device;
        }

        public void UpdateTransform()
        {
            if(!_device.isValid)
                return;
            
            if(_device.TryGetFeatureValue(CommonUsages.devicePosition, out var position))
                _view.transform.position = position;
            
            if(_device.TryGetFeatureValue(CommonUsages.deviceRotation, out var rotation))
                _view.transform.rotation = rotation;
        }
    }
}