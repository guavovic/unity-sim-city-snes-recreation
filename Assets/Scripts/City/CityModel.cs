public class CityModel
{
    public string Name { get; private set; } = "N/A";
    public CityType CurrentCityType { get; private set; } = CityType.Sleep;
    public string MayorName { get; private set; } = "N/A";
    public ClimateType CurrentClimateType { get; private set; } = ClimateType.Test;
    public MapInfo MapInfo { get; private set; } = null;

    public void SetCityName(string cityName) { Name = cityName; }
    public void SetCurrentCityType(CityType cityType) { CurrentCityType = cityType; }
    public void SetMayorName(string mayorName) { MayorName = mayorName; }
    public void SetCurrentClimateType(ClimateType climateType) { CurrentClimateType = climateType; }
    public void SetMapInfo(MapInfo mapInfo) { MapInfo = mapInfo; }
}