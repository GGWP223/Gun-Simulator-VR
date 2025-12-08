using UnityEngine;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public CharacterController Controller { get; private set; }
    }
}