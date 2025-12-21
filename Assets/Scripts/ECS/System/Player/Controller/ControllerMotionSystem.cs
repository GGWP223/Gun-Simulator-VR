using Components;
using ECS.Reference;
using ECS.Tag;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.System
{
    public class ControllerMotionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ControllerInputComponent, RigidbodyReference> _bodyFilter = null;
        private readonly EcsFilter<HeadTag, TransformReference> _headFilter = null;
        
        public void Run()
        {
            foreach (var entity in _bodyFilter)
            {
                var input = _bodyFilter.Get1(entity);
                var head = _headFilter.Get2(entity);
                var rigidbody = _bodyFilter.Get2(entity);
                
                var direction = head.Transform.rotation * new Vector3(input.Direction.x, 0f, input.Direction.y);
                var rb = rigidbody.Rigidbody;
      
                if (direction.sqrMagnitude > 1f)
                    direction.Normalize();

                var targetPos = rb.position + direction * Time.fixedDeltaTime;
                
                rb.MovePosition(targetPos);
            }
        }
    }
}