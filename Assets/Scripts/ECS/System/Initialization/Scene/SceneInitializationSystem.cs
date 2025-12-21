using Components;
using Leopotam.Ecs;

namespace ECS.System.Initialization.Scene
{
    public class SceneInitializationSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        
        public void Init()
        {
            var scene = _world.NewEntity();
            
            scene.Get<DevicesComponent>();
        }
    }
}