[System.Serializable]
public class UserScheduleData
{
    public int wakeHour;
    public int wakeMinute;
    
    public int sleepHour;
    public int sleepMinute;
    
    public int reminderIntervalMinutes;

    public bool initialized;
}
