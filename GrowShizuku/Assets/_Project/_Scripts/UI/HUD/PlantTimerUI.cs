using Shizuku.Managers;
using TMPro;
using UnityEngine;
using Utilities;

namespace Shizuku.UI
{
    public class PlantTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private IGrowthSource _plant;
        private CountdownTimer uiTimer;

        private void Start()
        {
            _plant = GrowthManager.Source;
            uiTimer = new CountdownTimer(0);
        }

        private void Update()
        {
            _timerText.text = FormatTime(RemainingTime());
        }

        private string FormatTime(float seconds)
        {
            int min = Mathf.FloorToInt(seconds / 60);
            int sec = Mathf.FloorToInt(seconds % 60);
            return $"{min:00}:{sec:00}";
        }

        private float RemainingTime()
        {
            return (float)_plant.TimeUntilNextWater();
        }
    }
}
