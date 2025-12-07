using UnityEngine;
using UnityEngine.XR;
using View;

namespace Services.Character.Hands
{
    public class PlayerHandsService : IPlayerHandsService
    {
        private readonly HandsView _view;
        
        private InputDevice _leftHandDevice;
        private InputDevice _rightHandDevice;

        public PlayerHandsService
        (
            HandsView view
        )
        {
            _view = view;
        }
        
        public void SetDevice(InputDevice device)
        {
            if ((device.characteristics & InputDeviceCharacteristics.Controller) == 0)
                return;

            if ((device.characteristics & InputDeviceCharacteristics.Left) != 0)
                _leftHandDevice = device;
            
            if ((device.characteristics & InputDeviceCharacteristics.Right) != 0)
                _rightHandDevice = device;
        }
        
        public void UpdateTransform()
        {
            SetTransform(_leftHandDevice, _view.LHand);
            SetTransform(_rightHandDevice, _view.RHand);
        }

        private void SetTransform(InputDevice device, Transform hand)
        {
            if(!device.isValid)
                return;
            
            if(device.TryGetFeatureValue(CommonUsages.devicePosition, out var position))
                hand.position = position;
            
            if(device.TryGetFeatureValue(CommonUsages.deviceRotation, out var rotation))
                hand.rotation = rotation;
        }
    }
}