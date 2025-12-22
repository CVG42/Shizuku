using System;
using UnityEngine;

namespace Shizuku.Managers
{
    public class UserScheduleManager : Singleton<IScheduleSource>, IScheduleSource
    {
        private const string SAVE_KEY = "user_schedule";

        public UserScheduleData Data { get; private set; }

        public int ReminderIntervalMinutes => Data.reminderIntervalMinutes;
        public bool IsInitialized => Data != null && Data.initialized;

        protected override void Awake()
        {
            base.Awake();
            LoadOrCreate();
        }

        private void LoadOrCreate()
        {
            if (SavingManager.TryLoad(SAVE_KEY, out UserScheduleData data) && data.initialized)
            {
                Data = data;
                return;
            }

            Data = new UserScheduleData()
            {
                wakeHour = 9,
                wakeMinute = 0,
                sleepHour = 21,
                sleepMinute = 0,
                reminderIntervalMinutes = 60,
                initialized = false
            };
        }

        public void ConfirmSchedule(int wakeHour, int wakeMinute, int sleepHour, int sleepMinute, int reminderIntervalMinutes)
        {
            Data.wakeHour = wakeHour;
            Data.wakeMinute = wakeMinute;
            Data.sleepHour = sleepHour;
            Data.sleepMinute = sleepMinute;
            Data.reminderIntervalMinutes = reminderIntervalMinutes;
            Data.initialized = true;

            Save();
        }

        public bool IsWithinAwakeWindow()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;

            TimeSpan wake = new TimeSpan(Data.wakeHour, Data.wakeMinute, 0);
            TimeSpan sleep = new TimeSpan(Data.sleepHour, Data.sleepMinute, 0);

            if (wake < sleep)
            {
                return now >= wake && now < sleep;
            }

            return now >= wake || now < sleep;
        }

        public void Save()
        {
            SavingManager.Save(SAVE_KEY, Data);
        }
    }
}