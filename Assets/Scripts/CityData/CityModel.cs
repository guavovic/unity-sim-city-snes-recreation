using System;
using UnityEngine;

public class CityModel
{
    public string Name { get; private set; }
    public CityType CurrentCityType { get; private set; }
    public string MayorName { get; private set; }
    public MapData MapData { get; private set; }
    public ClimateType CurrentClimateType { get; private set; }

    public CityModel(string name, string mayorName, MapData mapData)
    {
        this.Name = name;
        this.MayorName = mayorName;
        this.MapData = mapData;
    }
}