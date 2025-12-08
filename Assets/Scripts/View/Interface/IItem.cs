using Data;
using UnityEngine;

namespace View.Item.Interface
{
    public interface IItem
    {
        public ItemData Data { get; }
        
        public Rigidbody Rigidbody { get; }
        public Collider Collider { get; }
    }
}