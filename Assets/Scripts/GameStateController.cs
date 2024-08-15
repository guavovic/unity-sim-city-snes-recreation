public sealed class GameStateController
{
    public static GameState CurrentState { get; private set; } = GameState.None;

    public static void SetState(GameState newState) { CurrentState = newState; }

    public static bool IsState(GameState state) { return CurrentState == state; }

    public static bool IsBusy() { return CurrentState != GameState.None; }
}