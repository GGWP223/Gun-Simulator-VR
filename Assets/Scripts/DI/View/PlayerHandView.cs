using System;
using ECS.System.Initialization;
using UnityEngine;

namespace DI.View
{
    public class PlayerHandView : MonoBehaviour
    {
        [field: SerializeField] public Hand RHand { get; private set; }
        [field: SerializeField] public Hand LHand { get; private set; }
    }
    
    [Serializable]
    public class Hand
    {
        public Transform Transform;
        public ConfigurableJoint Joint;
    }
}