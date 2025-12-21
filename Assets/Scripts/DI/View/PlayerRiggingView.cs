using UnityEngine;

namespace DI.View
{
    public class PlayerRiggingView : MonoBehaviour
    {
        [field: SerializeField] public Rig BodyRig { get; private set; }
        [field: SerializeField] public Rig HeadRig { get; private set; }
        [field: SerializeField] public Rig LeftArmRig { get; private set; }
        [field: SerializeField] public Rig RightArmRig { get; private set; }
        [field: SerializeField] public Rig LeftLegRig { get; private set; }
        [field: SerializeField] public Rig RightLegRig { get; private set; }
    }
    
    [System.Serializable]
    public struct Rig
    {
        [field: SerializeField] public Transform Target { get; private set; }
        [field: SerializeField] public Transform Hint { get; private set; }
        
        [field: SerializeField] public Vector3 PositionOffset { get; private set; }
        [field: SerializeField] public Vector3 RotationOffset { get; private set; }
    }
}