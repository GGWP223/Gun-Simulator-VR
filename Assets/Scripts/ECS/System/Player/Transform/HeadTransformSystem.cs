using Components;
using ECS.Reference;
using ECS.Tag;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.XR;

namespace ECS.System
{
    public class HeadTransformSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HeadTag, TransformReference> _headFilter = null;
        
        private readonly EcsFilter<DevicesComponent> _devicesFilter = null;
        
        public void Run()
        {
            foreach (var entity in _devicesFilter)
            {
                var devices = _devicesFilter.Get1(entity);
                
                ref var head = ref _headFilter.Get2(entity);
                
                foreach (var device in devices.Devices)
                {
                    if((device.characteristics & InputDeviceCharacteristics.HeadMounted) == 0)
                        return;
                    
                    head.Transform.localPosition = device.TryGetFeatureValue(CommonUsages.devicePosition, out var position) ? position : Vector2.zero;
                    head.Transform.rotation = device.TryGetFeatureValue(CommonUsages.deviceRotation, out var rotation) ? rotation : Quaternion.identity;
                }
            }
        }
    }
}