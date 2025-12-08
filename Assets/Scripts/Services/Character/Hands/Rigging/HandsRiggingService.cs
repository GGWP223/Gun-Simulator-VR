using System.Collections.Generic;
using Data;
using Static;
using TMPro;
using UnityEngine;
using View;
using View.Item.Interface;
using Zenject;

namespace Services.Character.Hands.Rigging
{
    public class HandsRiggingService : IHandsRiggingService
    {
        private readonly FingersRigView[] _rigs;
        private readonly HandsView _hands;

        public HandsRiggingService
        (
            FingersRigView[] rigs,
            HandsView hands
        )
        {
            _rigs = rigs;
            _hands = hands;
        }

        public void SetFingersRig(IItem item, EHandType type)
        {
        
        }
    }
}