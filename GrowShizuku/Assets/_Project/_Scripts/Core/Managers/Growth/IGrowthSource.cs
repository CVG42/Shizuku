using System;

namespace Shizuku
{
    public interface IGrowthSource
    {
        PlantState CurrentPlantState { get; }

        event Action OnWatered;
        event Action<PlantDefinition> OnPlantCompleted;

        double TimeUntilNextWater();
        bool CanWater();

        void Water();
    }
}
