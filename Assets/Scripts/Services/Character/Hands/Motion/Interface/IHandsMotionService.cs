using Services.Input;
using UnityEngine.XR;

namespace Services.Character.Hands
{
    public interface IHandsMotionService
    {
        public void UpdateTransform(DeviceTransform rHand, DeviceTransform lHand);
    }
}