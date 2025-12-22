using System.Collections.Generic;

public interface ILogSource
{
    IReadOnlyCollection<string> CompletedPlants { get; }

    bool IsCompleted(PlantDefinition plant);

    void AddPlant(PlantDefinition plant);
}
