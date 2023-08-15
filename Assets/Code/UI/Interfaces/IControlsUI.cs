using GameCore.Player;

namespace UI
{
    public interface IControlsUI
    {
        void SetMoveInputListener(IMoveInputListener listener); 
        void SetFireListener(IFireInputListener fireInputListener); 
        
        void TurnOn();
        void TurnOff();
    }
}