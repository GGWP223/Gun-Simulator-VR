using DI.View;
using ECS.System.Initialization;
using Zenject;

namespace DI.Service.Player.Create
{
    public class PlayerInitializeService : IPlayerInitializeService
    {
        private readonly PlayerInitializationSystem _system;
        
        private readonly PlayerView _player;
        
        private readonly PlayerHeadView _head;
        private readonly PlayerHandView _hand;
        private readonly PlayerRiggingView _rigging;

        public PlayerInitializeService
        (
            PlayerInitializationSystem system,
            PlayerView player,
            PlayerHeadView head,
            PlayerHandView hand,
            PlayerRiggingView rigging
        )
        {
            _system = system;
            _player = player;
            _head = head;
            _hand = hand;
            _rigging = rigging;
        }
        
        public void Initialize()
        {
            var player = new PlayerInitializationRequest
            (
                _player,
                _head,
                _hand,
                _rigging
            );
            
            _system.SendRequest(player);
        }
    }
}