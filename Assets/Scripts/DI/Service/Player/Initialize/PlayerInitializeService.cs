using DI.View;
using ECS.System.Initialization;
using Zenject;

namespace DI.Service.Player.Create
{
    public class PlayerInitializeService : IPlayerInitializeService
    {
        private readonly PlayerInitializationSystem _system;
        private readonly PlayerView _view;

        public PlayerInitializeService
        (
            PlayerInitializationSystem system,
            PlayerView view
        )
        {
            _system = system;
            _view = view;
        }
        
        public void Initialize()
        {
            var request = new PlayerInitializationRequest
            (
                _view.Controller,
                _view.Controller.transform
            );
            
            _system.SendRequest(request);
        }
    }
}