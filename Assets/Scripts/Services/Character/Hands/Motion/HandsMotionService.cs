using Services.Input;
using UnityEngine;
using UnityEngine.XR;
using View;

namespace Services.Character.Hands
{
    public class HandsMotionService : IHandsMotionService
    {
        private readonly HandsView _hands;
        private readonly PlayerView _player;

        private InputDevice _leftHandDevice;
        private InputDevice _rightHandDevice;
        
        private Vector3 _leftHandPosition;
        private Vector3 _rightHandPosition;
        
        private Quaternion _leftHandRotation;
        private Quaternion _rightHandRotation;

        private const float PositionSmooth = 25f;
        private const float RotationSmooth = 35f;

        public HandsMotionService
        (
            HandsView hands,
            PlayerView player
        )
        {
            _hands = hands;
            _player = player;
        }
        
        public void UpdateTransform(DeviceTransform rHand, DeviceTransform lHand)
        {
            UpdateTransform(rHand, _hands.RHand, ref _rightHandPosition, ref _rightHandRotation);
            UpdateTransform(lHand, _hands.LHand, ref _leftHandPosition, ref _leftHandRotation);
        }

        private void UpdateTransform(DeviceTransform device, Transform hand, ref Vector3 position, ref Quaternion rotation)
        {
            var targetPosition = device.Position + _player.transform.position + _hands.Offset;
            var targetRotation = device.Rotation;

            position = Vector3.Lerp(position, targetPosition, PositionSmooth * Time.deltaTime);
            rotation = Quaternion.Slerp(rotation, targetRotation, RotationSmooth * Time.deltaTime);

            hand.position = position;
            hand.rotation = rotation;
        }
    }
}