using Static;
using UniRx;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        public ReactiveProperty<(bool, EHandType)> TriggerState { get; }
        public ReactiveProperty<(bool, EHandType)> GripState { get; }
        
        public Vector2 MovementDirection { get; }
        
        public DeviceTransform CameraTransform { get; }
        
        public DeviceTransform LHandTransform { get; }
        public DeviceTransform RHandTransform { get; }
    }
}