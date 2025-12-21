using DI.View;
using UnityEngine;

namespace ECS.System.Initialization
{
    public class PlayerInitializationRequest
    {
        public PlayerView Player { get; }
        public PlayerHeadView Head { get; }
        public PlayerHandView Hand { get; }
        public PlayerRiggingView Rigging { get; }

        public PlayerInitializationRequest
        (
            PlayerView player, 
            PlayerHeadView head, 
            PlayerHandView hand, 
            PlayerRiggingView rigging
        )
        {
            Player = player;
            Head = head;
            Hand = hand;
            Rigging = rigging;
        }
    }
}