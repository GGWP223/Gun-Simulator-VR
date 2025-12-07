using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace View
{
    public class HandsView : MonoBehaviour
    {
        [field: SerializeField] public Transform LHand { get; private set; }
        [field: SerializeField] public Transform RHand { get; private set; }
    }
}