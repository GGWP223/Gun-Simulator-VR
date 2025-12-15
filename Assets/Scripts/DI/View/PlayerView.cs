using UnityEngine;

namespace DI.View
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public CharacterController Controller { get; private set; }
    }
}