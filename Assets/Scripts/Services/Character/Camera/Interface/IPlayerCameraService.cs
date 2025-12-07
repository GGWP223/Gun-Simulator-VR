using UnityEngine.XR;

namespace Services.Camera
{
    public interface IPlayerCameraService
    {
        public void SetDevice(InputDevice device);

        public void UpdateTransform();
    }
}