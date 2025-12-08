using System.Collections.Generic;
using JetBrains.Annotations;
using Services.Character.Hands.Rigging;
using Services.Input;
using Static;
using UniRx;
using UnityEngine;
using View;
using View.Item.Interface;
using Zenject;

namespace Services.Character.Hands.Grab
{
    public class HandsGrabService : IHandsGrabService, IInitializable
    {
        private readonly Hand[] _hands;
        
        private readonly IInputService _input;
        private readonly IHandsRiggingService _rigging;
        
        private readonly HandsView _view;
        
        public HandsGrabService
        (
            IInputService input,
            IHandsRiggingService rigging,
            HandsView view
        )
        {
            _input = input;
            _rigging = rigging;
            _view = view;

            _hands = new Hand[]
            {
                new(EHandType.Left),
                new(EHandType.Right),
            };
        }

        public void Initialize()
        {
            _input.GripState
                .Subscribe(grip => OnGripStateChanged(grip.Item1, grip.Item2))
                .AddTo(_view);
        }

        public void Update()
        {
            foreach (var hand in _hands)
            {
                var item = hand.Item;
                
                _rigging.SetFingersRig(hand.Item, hand.Type);
                
                if(item is null)
                    continue;

                var transform = GetHandTransform(hand);
                
                item.Collider.transform.position = transform.TransformPoint(item.Data.PositionOffset);
                item.Collider.transform.rotation = transform.rotation * Quaternion.Euler(item.Data.RotationOffset);
            }
        }

        public Hand GetHand(EHandType type)
        {
            foreach (var hand in _hands)
            {
                if(type == hand.Type)
                    return hand;
            }
            
            return null;
        }

        private void OnGripStateChanged(bool state, EHandType type)
        {
            var hand = GetHand(type);
            
            if(state)
                TryPickupItem(hand);
            else
            {return;
                TryDropItem(hand);}
        }

        private void TryPickupItem(Hand hand)
        {
            if(hand.Item is not null)
                return;
            
            hand.Item = GetNearestItems(hand);
            
            if(hand.Item is null)
                return;
            
            ChangeItemState(true, hand);
        }

        private void TryDropItem(Hand hand)
        {
            if(hand?.Item is null)
                return;
            
            ChangeItemState(false, hand);
            
            hand.Item = null;
        }

        private IItem GetNearestItems(Hand hand)
        {
            var radius = 1f;
            var mask = LayerMask.GetMask("Default");

            var position = GetHandTransform(hand).position;
            var size = Physics.OverlapSphereNonAlloc(position, radius, hand.Buffer, mask);
            
            var minDist = float.MaxValue;
            
            IItem nearest = null;

            for(var i = 0; i < size; i++)
            {
                var collider = hand.Buffer[i];
                
                if(!collider.TryGetComponent<IItem>(out var item))
                    continue;
                
                var distance = Vector3.Distance(position, item.Rigidbody.position);

                if (!(distance < minDist)) 
                    continue;
                
                minDist = distance;
                nearest = item;
            }
            return nearest;
        }

        private void ChangeItemState(bool isGrabbed, Hand hand)
        {
            if(hand?.Item is null)
                return;
            
            hand.Item.Rigidbody.isKinematic = isGrabbed;
            hand.Item.Collider.enabled = !isGrabbed;
        }

        private Transform GetHandTransform(Hand hand)
        {
            switch (hand.Type)
            {
                case EHandType.Right:
                    return _view.RHand;
                case EHandType.Left:
                    return _view.LHand;
            }
            
            return null;
        }
    }
}