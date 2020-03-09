using ProjectSP0.Manager;
using System;

namespace ProjectSP0
{
    public class TemporaryStatus 
    {
        public class TemporaryStatusInfo
        {
            public StatusType statusType = StatusType.HP;
            public int value = 0;
            public int time = 0;
        }

        public enum StatusType
        {
            HP,
            Attack,
            Defence,
            Dex
        }

        public event Action<TemporaryStatus> OnTimeUp = null;

        public readonly StatusType statusType = StatusType.HP;
        public readonly int value = 0;
        private int m_time = 0;

        public TemporaryStatus(TemporaryStatusInfo info)
        {
            statusType = info.statusType;
            value = info.value;
            m_time = info.time;

            GameManager.Instance.OnMinutesPassed += OnTimePassed;
        }

        ~TemporaryStatus()
        {
            GameManager.Instance.OnMinutesPassed -= OnTimePassed;
        }

        private void OnTimePassed(int minutes)
        {
            m_time -= minutes;
            if (m_time <= 0)
            {
                OnTimeUp?.Invoke(this);
            }
        }
    }
}
