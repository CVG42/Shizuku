using System;
using Shizuku.Managers;
using Shizuku.Services;
using UnityEngine;

namespace Shizuku.Timers
{
    public class HydrationTimer : MonoBehaviour
    {
        private const string SAVE_KEY = "hydration";

        [SerializeField] private float dryAfterSeconds = 3600f;

        public HydrationState CurrentState { get; private set; } = HydrationState.Normal;

        public event Action<HydrationState> OnStateChanged;

        private double _lastWaterTime;

        private void Start()
        {
            Load();
            GrowthManager.Source.OnWatered += OnWatered;
        }

        private void Update()
        {
            Evaluate();
        }

        private void Evaluate()
        {
            if (CurrentState == HydrationState.Dry) return;

            if (TimeService.Now - _lastWaterTime >= dryAfterSeconds)
            {
                SetState(HydrationState.Dry);
            }
        }

        private void OnWatered()
        {
            _lastWaterTime = TimeService.Now;

            if (CurrentState != HydrationState.Normal)
            {
                SetState(HydrationState.Normal);
            }

            Save();
        }

        private void SetState(HydrationState state)
        {
            CurrentState = state;
            OnStateChanged?.Invoke(state);
        }

        private void Save()
        {
            SavingManager.Save(SAVE_KEY, new HydrationSaveData
            {
                lastWaterTime = _lastWaterTime
            });
        }

        private void Load()
        {
            if (!SavingManager.TryLoad(SAVE_KEY, out HydrationSaveData data))
            {
                _lastWaterTime = TimeService.Now;
                return;
            }

            _lastWaterTime = data.lastWaterTime;
        }

        private void OnDestroy()
        {
            GrowthManager.Source.OnWatered -= OnWatered;
        }
    }
}

[System.Serializable]
public class HydrationSaveData
{
    public double lastWaterTime;
}