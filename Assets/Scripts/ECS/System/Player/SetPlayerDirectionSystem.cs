using Components;
using ECS.Tag;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.System
{
    public class SetPlayerDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerInputComponent, CharacterComponent> _bodyFilter = null;
        private readonly EcsFilter<HeadTag, TransformComponent> _headFilter = null;
        
        public void Run()
        {
            foreach (var entity in _bodyFilter)
            {
                var input = _bodyFilter.Get1(entity);
                var head = _headFilter.Get2(entity);
                
                ref var character = ref _bodyFilter.Get2(entity);
                
                var direction = head.Rotation * new Vector3(input.Direction.x, 0, input.Direction.y).normalized;
        
                character.Velocity.x = direction.x * Time.deltaTime;
                character.Velocity.z = direction.z * Time.deltaTime;
            }
        }
    }
}