using UnityEngine;

namespace Shizuku
{
    public interface IGrowthSource
    {
        PlantState CurrentPlantState { get; }

        double TimeUntilNextWater();
        bool CanWater();

        void Water();
    }
}
