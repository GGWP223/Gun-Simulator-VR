using Services.Input;
using View;

namespace Services.Camera
{
    public class PlayerCameraService : IPlayerCameraService
    {
        private readonly CameraView _camera;
        private readonly PlayerView _player;

        public PlayerCameraService
        (
            CameraView camera,
            PlayerView player
        )
        {
            _camera = camera;
            _player = player;
        }

        public void UpdateTransform(DeviceTransform transform)
        {
            _camera.CameraComponent.transform.position = transform.Position + _player.transform.position;
            _camera.CameraComponent.transform.rotation = transform.Rotation;
        }
    }
}