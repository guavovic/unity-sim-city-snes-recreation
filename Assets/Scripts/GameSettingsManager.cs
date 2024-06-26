using UnityEngine;

public sealed class GameSettings
{
    public CityModel CurrentCity { get; private set; }
    public float GameSpeed { get; private set; } = 1f;

    public void SetCurrentCity(CityModel city) { CurrentCity = city; }
    public void ModifyGameSpeed(float speed) { GameSpeed = speed; }
}
