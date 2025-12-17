using Shizuku.Services;
using UnityEngine;

namespace Shizuku.Managers
{
    public class GrowthManager : Singleton<IGrowthSource>, IGrowthSource
    {
        public PlantState CurrentPlantState { get; private set; } = PlantState.Seed;

        [SerializeField] private float _wateringCooldown = 900f;

        private double _nextWaterTime;

        private void Start()
        {
            _nextWaterTime = TimeService.Now;
            EnterPlantState(CurrentPlantState);
        }

        public bool CanWater()
        {
            return TimeService.Now >= _nextWaterTime && CurrentPlantState != PlantState.Complete;
        }

        public double TimeUntilNextWater()
        {
            return Mathf.Max(0f, (float)(_nextWaterTime - TimeService.Now));
        }

        public void Water()
        {
            if (!CanWater()) return;
            
            NextPlantState();
            _nextWaterTime = TimeService.Now + _wateringCooldown;
        }

        private void NextPlantState()
        {
            if (CurrentPlantState == PlantState.Complete) return;

            CurrentPlantState++;
            EnterPlantState(CurrentPlantState);
        }

        private void EnterPlantState(PlantState plantState)
        {
            Debug.Log($"Plant has entered {plantState} state.");
        }
    }
}
