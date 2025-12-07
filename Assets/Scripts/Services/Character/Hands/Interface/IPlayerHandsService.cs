using UnityEngine.XR;

namespace Services.Character.Hands
{
    public interface IPlayerHandsService
    {
        public void SetDevice(InputDevice device);
        
        public void UpdateTransform();
    }
}