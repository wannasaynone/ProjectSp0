using System;

namespace ProjectSP0.Manager
{
    public class GameManager : KahaGameCore.Interface.Manager
    {
        public static GameManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new GameManager();
                }
                return m_instance;
            }
        }
        private static GameManager m_instance = null;
        private GameManager() { }

        public Character CurrentCharacter { get; private set; } = new Character("TestingPlayer");

        public Int32ValueObject CurrentDay { get; private set; } = new Int32ValueObject(1);
        public Int32ValueObject CurrentHour { get; private set; } = new Int32ValueObject(0);
        public Int32ValueObject CurrentMinute { get; private set; } = new Int32ValueObject(0);

        public event Action<int> OnMinutesPassed = null;
        public event Action<int> OnHoursPassed = null;
        public event Action<int> OnDaysPassed = null;

        public void AddMinutes(int minutes)
        {
            CurrentMinute.Value += minutes;
            while(CurrentMinute.Value >= 60)
            {
                CurrentMinute.Value -= 60;
                AddHours(1);
            }
            OnMinutesPassed?.Invoke(minutes);
        }

        public void AddHours(int hours)
        {
            CurrentHour.Value += hours;
            while(CurrentHour.Value >= 24)
            {
                CurrentHour.Value -= 24;
                AddDays(1);
            }
            OnHoursPassed?.Invoke(hours);
        }

        public void AddDays(int days)
        {
            CurrentDay.Value++;
            OnDaysPassed?.Invoke(days);
        }
    }
}
