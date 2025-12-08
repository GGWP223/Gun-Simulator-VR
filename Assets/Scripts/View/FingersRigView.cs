using System;
using System.Collections.Generic;
using Static;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace View
{
    public class FingersRigView : MonoBehaviour
    {
        [field: SerializeField] public EHandType HandType { get; private set; }
        [field: SerializeField] public FingerView[] Fingers { get; private set; }
    }

    [Serializable]
    public struct FingerView
    {
        [field: SerializeField] public EFingerType Type  { get; private set; }
        [field: SerializeField] public TwoBoneIKConstraint Target  { get; private set; }
    }
}