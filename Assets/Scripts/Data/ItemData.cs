using System;
using System.Collections.Generic;
using Static;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(ItemData), menuName = "Item/" + nameof(ItemData))]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public Vector3 PositionOffset { get; private set; }
        [field: SerializeField] public Vector3 RotationOffset { get; private set; }

        [field: SerializeField] public List<Finger> Fingers;
    }

    [Serializable]
    public struct Finger
    {
        [field: SerializeField] public EFingerType Type  { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Vector3 Rotation { get; private set; }
    }
}