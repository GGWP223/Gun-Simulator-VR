using Static;
using View.Item.Interface;

namespace Services.Character.Hands.Rigging
{
    public interface IHandsRiggingService
    {
        public void SetFingersRig(IItem item, EHandType type);
    }
}