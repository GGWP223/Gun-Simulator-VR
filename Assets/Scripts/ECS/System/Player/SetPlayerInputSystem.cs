using Components;
using ECS.Tag;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.System
{
    public class SetPlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerInputComponent> _inputFilter;
        private readonly EcsFilter<RightHandTag, HandInputComponent> _handFilter;
        
        public void Run()
        {
            foreach (var entity in _inputFilter)
            {
                ref var input = ref _inputFilter.Get1(entity);
                
                var hand = _handFilter.Get2(entity);
                
                input.Direction = hand.Joystick == Vector2.zero ?
                    new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) : 
                    new Vector2(hand.Joystick.x, hand.Joystick.y);
            }
        }
    }
}