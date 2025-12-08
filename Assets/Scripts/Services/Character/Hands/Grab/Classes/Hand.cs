using Static;
using UnityEngine;
using View.Item.Interface;

namespace Services.Character.Hands.Grab
{
    public class Hand
    {
        public EHandType Type { get; }
        public Collider[] Buffer { get; }
        public IItem Item { get; set; }

        public Hand(EHandType type)
        {
            Type = type;
            Buffer = new Collider[8];
        }
    }
}