using Shizuku.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserScheduleDebug : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private TMP_InputField wakeHour;
    [SerializeField] private TMP_InputField wakeMinute;
    [SerializeField] private TMP_InputField sleepHour;
    [SerializeField] private TMP_InputField sleepMinute;
    [SerializeField] private TMP_InputField intervalMinutes;

    [Header("UI")]
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Button saveButton;

    private void Start()
    {
        PopulateFields();
        saveButton.onClick.AddListener(OnSaveClicked);
    }

    private void PopulateFields()
    {
        if (!UserScheduleManager.Source.IsInitialized)
        {
            statusText.text = "Schedule not initialized";
            return;
        }

        var data = UserScheduleManager.Source.Data;

        wakeHour.text = data.wakeHour.ToString();
        wakeMinute.text = data.wakeMinute.ToString();
        sleepHour.text = data.sleepHour.ToString();
        sleepMinute.text = data.sleepMinute.ToString();
        intervalMinutes.text = data.reminderIntervalMinutes.ToString();

        statusText.text = "Loaded saved schedule";
    }

    private void OnSaveClicked()
    {
        if (!TryParse(out int wh, out int wm, out int sh, out int sm, out int interval))
        {
            statusText.text = "Invalid input values";
            return;
        }

        UserScheduleManager.Source.ConfirmSchedule(wh, wm, sh, sm, interval);
        statusText.text = "Schedule saved";
    }

    private bool TryParse(
    out int wakeH,
    out int wakeM,
    out int sleepH,
    out int sleepM,
    out int interval
)
    {
        bool validWakeH = int.TryParse(wakeHour.text, out wakeH);
        bool validWakeM = int.TryParse(wakeMinute.text, out wakeM);
        bool validSleepH = int.TryParse(sleepHour.text, out sleepH);
        bool validSleepM = int.TryParse(sleepMinute.text, out sleepM);
        bool validInterval = int.TryParse(intervalMinutes.text, out interval);

        return validWakeH
            && validWakeM
            && validSleepH
            && validSleepM
            && validInterval;
    }
}
