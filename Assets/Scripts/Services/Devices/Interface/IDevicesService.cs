using UniRx;
using UnityEngine.XR;

namespace Services.Devices
{
    public interface IDevicesService
    {
        public ReactiveCommand<InputDevice> OnDeviceConnected { get; }
        
        public ReactiveCommand<InputDevice> OnDeviceDisconnected { get; }
    }
}