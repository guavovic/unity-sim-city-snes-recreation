public sealed class GameSettings
{
    public City City { get; private set; } = null;
    public GameDifficultyType GameDifficultyType { get; private set; } = GameDifficultyType.None;
    public GameSpeedType GameSpeedType { get; private set; } = GameSpeedType.Sleep;

    public void SetCity(City city) {  City = city; }
    public bool CityCreated() { return City != null; }
    public void SetGameDifficultyType(GameDifficultyType difficultyType) { GameDifficultyType = difficultyType; }
    public void ModifyGameSpeedType(GameSpeedType speedType) { GameSpeedType = speedType; }
}