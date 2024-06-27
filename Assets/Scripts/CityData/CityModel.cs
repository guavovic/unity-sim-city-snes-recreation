using System;
using UnityEngine;

public sealed class CityModel
{
    public string Name { get; private set; }
    public CityType CurrentCityType { get; private set; }
    public string MayorName { get; private set; }
    public MapData MapData { get; private set; }
    public ClimateType CurrentClimateType { get; private set; }

    public CityModel(string name, string mayorName, MapData mapData)
    {
        Name = name;
        MayorName = mayorName;
        MapData = mapData;
    }

    public void SetCurrentCityType(CityType cityType) { CurrentCityType = cityType; }

    public void SetCurrentClimateType(ClimateType climateType) { CurrentClimateType = climateType; }
}