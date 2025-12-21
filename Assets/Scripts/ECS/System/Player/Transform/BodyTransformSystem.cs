using Components;
using ECS.Reference;
using ECS.Tag;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.System
{
    public class BodyTransformSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HeadTag, TransformReference> _headFilter = null;
        private readonly EcsFilter<BodyTag, ColliderReference> _bodyFilter = null;
        
        public void Run()
        {
            foreach (var i in _bodyFilter)
            {
                ref var reference = ref _bodyFilter.Get2(i);

                foreach (var j in _headFilter)
                {
                    ref var head = ref _headFilter.Get2(j);
                    
                    if(reference.Collider is not CapsuleCollider collider)
                        return;

                    collider.height = Mathf.Clamp(head.Transform.localPosition.y + 0.1f, 0.5f, 2f);
                    collider.center = new Vector3(head.Transform.localPosition.x, collider.height * 0.5f, head.Transform.localPosition.z);
                }
            }
        }
    }
}