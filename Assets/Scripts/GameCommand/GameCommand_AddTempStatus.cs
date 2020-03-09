using System;
using UnityEngine;

namespace ProjectSP0.GameCommand
{
    public class GameCommand_AddTempStatus : GameCommandBase
    {
        [SerializeField] private TemporaryStatus.StatusType m_statusType = TemporaryStatus.StatusType.HP;
        [SerializeField] private int m_value = 0;
        [SerializeField] private int m_time = 0;

        public override void Process(Action onComplete)
        {
            receiver.GameBuffManager.AddTempValue(new TemporaryStatus.TemporaryStatusInfo
            {
                statusType = m_statusType,
                time = m_time,
                value = m_value
            });

            onComplete?.Invoke();
        }
    }
}
