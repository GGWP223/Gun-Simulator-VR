using Data;
using UnityEngine;
using View.Item.Interface;

namespace View
{
    public class KnifeView : MonoBehaviour, IItem
    {
        [field: SerializeField] public ItemData Data { get; private set; }
        
        public Rigidbody Rigidbody { get; private set; }
        public Collider Collider { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
        }
    }
}