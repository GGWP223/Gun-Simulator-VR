using Components;
using ECS.Reference;
using Leopotam.Ecs;

namespace ECS.System
{
    public class SyncCharacterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CharacterReference, CharacterComponent> _filter = null;
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                var reference = _filter.Get1(entity);
                var character = _filter.Get2(entity);
                
                reference.Controller.Move(character.Velocity);
            }
        }
    }
}