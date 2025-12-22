using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shizuku.Managers
{
    public class PlantLogManager : Singleton<ILogSource>, ILogSource
    {
        private const string SAVE_KEY = "plant_log";

        private HashSet<string> _completedPlants = new();

        public IReadOnlyCollection<string> CompletedPlants => _completedPlants;

        private void Start()
        {
            GrowthManager.Source.OnPlantCompleted += AddPlant;

            Load();
        }

        public bool IsCompleted(PlantDefinition plant)
        {
            return _completedPlants.Contains(plant.Id);
        }

        public void AddPlant(PlantDefinition plant)
        {
            if (_completedPlants.Add(plant.Id))
            {
                Debug.Log($"Plant added to log: {plant.PlantName}");
                Save();
            }
        }

        private void Save()
        {
            PlantLogData data = new PlantLogData
            {
                completedPlants = _completedPlants.ToList()
            };

            SavingManager.Save(SAVE_KEY, data);
        }

        private void Load()
        {
            if (!SavingManager.TryLoad(SAVE_KEY, out PlantLogData data)) return;

            _completedPlants = new HashSet<string>(data.completedPlants);
        }

        private void OnDestroy()
        {
            GrowthManager.Source.OnPlantCompleted -= AddPlant;
        }
    }
}