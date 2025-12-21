using UnityEngine;

namespace DI.View
{
    public class PlayerHeadView : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
    }
}