public interface IScheduleSource
{
    UserScheduleData Data { get; }

    int ReminderIntervalMinutes { get; }
    bool IsInitialized { get; }
    bool IsWithinAwakeWindow();

    void ConfirmSchedule(int wakeHour, int wakeMinute, int sleepHour, int sleepMinute, int reminderIntervalMinutes);
    void Save();
}
