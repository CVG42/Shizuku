using System;
using Shizuku.Services;
using UnityEngine;

namespace Shizuku.Managers
{
    public class GrowthManager : Singleton<IGrowthSource>, IGrowthSource
    {
        public PlantState CurrentPlantState => _currentPlant.PlantStates[_currentStateIndex].state;

        [SerializeField] private float _wateringCooldown = 900f;

        [Header("Plants data")]
        [SerializeField] private PlantDatabase _plantDatabase;
        [SerializeField] private PlantDefinition _currentPlant;

        public event Action<PlantDefinition> OnPlantCompleted;

        private double _nextWaterTime;
        private int _currentStateIndex;
        private int _wateringsInCurrentState;

        private void Start()
        {
            if (!LoadState())
            {
                InitializeNewGame();
            }
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

            _wateringsInCurrentState++;

            var stateData = _currentPlant.PlantStates[_currentStateIndex];

            if (_wateringsInCurrentState >= stateData.wateringsRequired)
            {
                AdvanceState();
            }

            _nextWaterTime = TimeService.Now + _wateringCooldown;
            SaveState();
        }

        private void AdvanceState()
        {
            _wateringsInCurrentState = 0;
            _currentStateIndex++;

            if (_currentStateIndex >= _currentPlant.PlantStates.Length)
            {
                CompletePlant();
                return;
            }

            EnterPlantState(CurrentPlantState);
        }

        private void InitializeNewGame()
        {
            _currentPlant = _plantDatabase.Plants[0];
            _currentStateIndex = 0;
            _wateringsInCurrentState = 0;
            _nextWaterTime = TimeService.Now;

            EnterPlantState(CurrentPlantState);
        }

        private void CompletePlant()
        {
            OnPlantCompleted?.Invoke(_currentPlant);

            PlantDefinition next = _plantDatabase.GetNext(_currentPlant);
            if (next == null)
            {
                Debug.Log("All plants completed!");
                return;
            }

            _currentPlant = next;
            _currentStateIndex = 0;
            _wateringsInCurrentState = 0;
            _nextWaterTime = TimeService.Now;

            EnterPlantState(CurrentPlantState);
        }

        private void EnterPlantState(PlantState plantState)
        {
            Debug.Log($"Plant has entered {plantState} state.");
        }

        private bool LoadState()
        {
            if (!SavingManager.TryLoad<SaveData>("growth", out var data))
                return false;

            _currentPlant = _plantDatabase.GetById(data.currentPlantId);

            if (_currentPlant == null)
            {
                Debug.LogWarning("Saved plant not found, starting new game.");
                return false;
            }

            _currentStateIndex = Mathf.Clamp(
                data.stateIndex,
                0,
                _currentPlant.PlantStates.Length - 1
            );

            _wateringsInCurrentState = data.wateringsInState;
            _nextWaterTime = data.nextWaterTime;

            EnterPlantState(CurrentPlantState);
            return true;
        }

        private void SaveState()
        {
            SaveData data = new SaveData
            {
                currentPlantId = _currentPlant.Id,
                stateIndex = _currentStateIndex,
                wateringsInState = _wateringsInCurrentState,
                nextWaterTime = _nextWaterTime
            };

            SavingManager.Save("growth", data);
        }

        private void OnApplicationQuit()
        {
            SaveState();
        }
    }
}
