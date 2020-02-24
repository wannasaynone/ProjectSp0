using System.Collections.Generic;

namespace ProjectSP0.Manager
{
    public class GameBuffManager
    {
        private readonly ICombatUnit m_owner = null;
        private List<GameBuffContainer> m_ownBuffs = new List<GameBuffContainer>();

        public GameBuffManager(ICombatUnit unit)
        {
            m_owner = unit;
        }

        public void AddBuff(GameBuff buff)
        {
            GameBuffContainer _buffContainer = m_ownBuffs.Find(x => x.info.buff == buff);
            if (_buffContainer != null)
            {
                _buffContainer.info.layCount++;
                _buffContainer.info.remainingTime = buff.ExistTime;
            }
            else
            {
                m_ownBuffs.Add(new GameBuffContainer(new GameBuffContainer.BuffInfo()
                {
                    buff = buff,
                    layCount = 1,
                    remainingTime = buff.ExistTime,
                    owner = m_owner
                }));

                m_ownBuffs[m_ownBuffs.Count - 1].OnBuffTimeUp += RemoveBuff;
                m_ownBuffs[m_ownBuffs.Count - 1].Active();
            }
        }

        public void RemoveBuff(GameBuffContainer buffContainer)
        {
            if (!buffContainer.Actived)
            {
                buffContainer.Deactive();
            }

            buffContainer.OnBuffTimeUp -= RemoveBuff;
            m_ownBuffs.Remove(buffContainer);
        }

        public void RemoveBuff(GameBuff buff, bool allLayCount = true)
        {
            GameBuffContainer _buffContainer = m_ownBuffs.Find(x => x.info.buff == buff);
            if (_buffContainer != null)
            {
                if (!allLayCount)
                {
                    _buffContainer.info.layCount--;
                }
                else
                {
                    m_ownBuffs.Remove(_buffContainer);
                }
            }
        }
    }
}
