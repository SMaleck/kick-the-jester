using Assets.Source.GameLogic;

namespace Assets.Source.Models
{
    public delegate void GameStateEventHandler(GameStateMachine.GameState state);

    public delegate void NotifyEventHandler();
    public delegate void IntValueEventHandler(int value);
    public delegate void FloatValueEventHandler(float value);
    public delegate void BoolValueEventHandler(bool value);

    public delegate void UnitEventHandler(UnitMeasurement value);
    public delegate void SpeedEventHandler(SpeedMeasurement value);
}
