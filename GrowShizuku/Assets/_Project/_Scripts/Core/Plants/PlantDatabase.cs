using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantDatabase", menuName = "Grow Shizuku/Plants/PlantDatabase")]
public class PlantDatabase : ScriptableObject
{
    [SerializeField] private PlantDefinition[] _plants;

    private Dictionary<string, PlantDefinition> _lookup;

    public IReadOnlyList<PlantDefinition> Plants => _plants;

    public PlantDefinition GetById(string id)
    {
        BuildLookup();
        return _lookup.TryGetValue(id, out var plant) ? plant : null;
    }

    public PlantDefinition GetNext(PlantDefinition currentPlant)
    {
        int index = Array.IndexOf(_plants, currentPlant);

        if (index < 0 || index + 1 >= _plants.Length) return null;

        return _plants[index + 1];
    }

    private void BuildLookup()
    {
        if (_lookup != null) return;

        _lookup = new Dictionary<string, PlantDefinition>();

        foreach (var plant in _plants)
        {
            _lookup[plant.Id] = plant;
        }
    }
}
