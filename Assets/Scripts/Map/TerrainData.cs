using System;
using System.Collections.Generic;

public sealed class TerrainData
{
    private readonly Dictionary<TerrainType, (Func<int> GetAmount, Action<int> SetAmount)> _terrainActions;

    public TerrainData()
    {
        _terrainActions = new Dictionary<TerrainType, (Func<int>, Action<int>)>
        {
            { TerrainType.Land, (() => LandAmount, value => LandAmount = value) },
            { TerrainType.Grass, (() => GrassAmount, value => GrassAmount = value) },
            { TerrainType.Forest, (() => ForestAmount, value => ForestAmount = value) },
            { TerrainType.Shore, (() => ShoreAmount, value => ShoreAmount = value) },
            { TerrainType.Water, (() => WaterAmount, value => WaterAmount = value) }
        };
    }

    public int LandAmount { get; private set; }
    public int GrassAmount { get; private set; }
    public int ForestAmount { get; private set; }
    public int ShoreAmount { get; private set; }
    public int WaterAmount { get; private set; }

    public void AddAmountTerrainType(TerrainType terrainType, int amount = 1)
    {
        if (_terrainActions.TryGetValue(terrainType, out var actions))
        {
            actions.SetAmount(actions.GetAmount() + amount);
        }
    }

    public void RemoveAmountTerrainType(TerrainType terrainType, int amount = 1)
    {
        if (_terrainActions.TryGetValue(terrainType, out var actions))
        {
            int newAmount = Math.Max(0, actions.GetAmount() - amount);
            actions.SetAmount(newAmount);
        }
    }
}
