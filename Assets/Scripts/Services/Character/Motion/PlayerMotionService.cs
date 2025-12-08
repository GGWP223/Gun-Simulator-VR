using UnityEngine;
using UnityEngine.InputSystem;
using View;
using Zenject;

namespace Services.Character.Motion
{
    public class PlayerMotionService : IPlayerMotionService
    {
        private readonly PlayerView _player;
        private readonly CameraView _camera;

        const float accel = 12f;
        const float maxSpeed = 1f;
        const float gravity = -9.81f;
        const float fallMultiplier = 2f;
        
        private float _verticalVelocity;
        private Vector2 _smoothDirection;

        public PlayerMotionService
        (
            PlayerView player,
            CameraView camera
        )
        {
            _player = player;
            _camera = camera;
        }
        
        public void UpdateTransform(Vector2 direction)
        {
            _smoothDirection = Vector2.Lerp(_smoothDirection, direction * maxSpeed, accel * Time.deltaTime);

            if (_player.Controller.isGrounded)
                _verticalVelocity = -0.5f;
            else
                _verticalVelocity += gravity * fallMultiplier * Time.deltaTime;

            var forward = _camera.CameraComponent.transform.forward;
            var right = _camera.CameraComponent.transform.right;

            var move = forward * _smoothDirection.y + right * _smoothDirection.x;

            _player.Controller.Move((move + Vector3.up * _verticalVelocity) * Time.deltaTime);
        }
    }
}