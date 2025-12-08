using Static;

namespace Services.Character.Hands.Grab
{
    public interface IHandsGrabService
    {
        public Hand GetHand(EHandType type);
        public void Update();
    }
}