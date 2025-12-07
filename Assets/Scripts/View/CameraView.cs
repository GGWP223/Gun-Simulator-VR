using UnityEngine;

namespace View
{
    public class CameraView : MonoBehaviour
    {
        [field: SerializeField] public Camera CameraComponent { get; private set; }
    }
}