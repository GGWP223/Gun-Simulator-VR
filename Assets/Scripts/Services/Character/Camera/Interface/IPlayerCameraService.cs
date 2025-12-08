using Services.Input;
using UnityEngine;
using UnityEngine.XR;

namespace Services.Camera
{
    public interface IPlayerCameraService
    {
        public void UpdateTransform(DeviceTransform transform);
    }
}