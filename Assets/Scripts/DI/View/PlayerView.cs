using UnityEngine;

namespace DI.View
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public CapsuleCollider Collider { get; private set; }
    }
}