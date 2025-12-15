using Components;
using ECS.Reference;
using Leopotam.Ecs;

namespace ECS.System
{
    public class SyncTransformSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformReference, TransformComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var reference = ref _filter.Get1(i);
                ref var component = ref _filter.Get2(i);
                
                reference.Transform.position = component.Position;
                reference.Transform.rotation = component.Rotation;
            }
        }
    }
}