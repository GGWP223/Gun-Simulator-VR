using System;
using Services.Camera;
using Services.Character.Hands;
using Services.Character.Hands.Grab;
using Services.Character.Motion;
using Services.Devices;
using Services.Input;
using Services.Loop;
using UniRx;

namespace Services.Character.Controller
{
    public class PlayerControllerService : IPlayerControllerService, IEveryUpdate, IEveryLateUpdate
    {
        private readonly IInputService _input;
        private readonly IPlayerCameraService _camera;
        private readonly IPlayerMotionService _motion;
        private readonly IHandsMotionService _handsMotion;
        private readonly IHandsGrabService _handsGrab;

        public PlayerControllerService
        (
            IInputService input,
            IPlayerCameraService camera,
            IPlayerMotionService motion,
            IHandsMotionService handsMotion,
            IHandsGrabService handsGrab
        )
        {
            _input = input;
            _camera = camera;
            _motion = motion;
            _handsMotion = handsMotion;
            _handsGrab = handsGrab;
        }
        
        public void Update()
        {
            _motion.UpdateTransform(_input.MovementDirection);
            _handsMotion.UpdateTransform(_input.RHandTransform, _input.LHandTransform);
            _handsGrab.Update();
        }

        public void LateUpdate()
        {
            _camera.UpdateTransform(_input.CameraTransform);
        }
    }
}