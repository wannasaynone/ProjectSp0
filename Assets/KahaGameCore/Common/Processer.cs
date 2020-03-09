using System;
using KahaGameCore.Interface;

namespace KahaGameCore.Common
{
    public class Processer<T> where T : IProcessable
    {
        private readonly T[] m_processableItems = null;

        private Action m_onDone = null;
        private int m_currentIndex = -1;

        public Processer(T[] items)
        {
            m_processableItems = items;
        }

        public void Start(Action onCompleted)
        {
            if(m_currentIndex != -1)
            {
                return;
            }
            m_onDone = onCompleted;
            RunGameCommands();
        }

        private void RunGameCommands()
        {
            m_currentIndex++;
            if (m_currentIndex >= m_processableItems.Length)
            {
                if (m_onDone != null)
                {
                    m_onDone();
                }

                m_currentIndex = -1;
                return;
            }

            if(m_processableItems[m_currentIndex] == null)
            {
                UnityEngine.Debug.LogErrorFormat("m_processableItems[{0}] == null", m_currentIndex);
                RunGameCommands();
                return;
            }

            m_processableItems[m_currentIndex].Process(RunGameCommands);
        }
    }

    public class GameCommandProcesser<T> where T : ProjectSP0.GameCommand.GameCommandBase, IProcessable
    {
        public class ProcesserInfo
        {
            public ProjectSP0.ICombatUnit catser = null;
            public ProjectSP0.ICombatUnit receiver = null;
            public Action onCompleted = null;
        }

        private readonly T[] m_processableItems = null;

        private ProcesserInfo m_info = null;
        private int m_currentIndex = -1;

        public GameCommandProcesser(T[] items)
        {
            m_processableItems = items;
        }

        public void Start(ProcesserInfo info)
        {
            if (m_currentIndex != -1)
            {
                return;
            }
            m_info = info;
            RunGameCommands();
        }

        private void RunGameCommands()
        {
            m_currentIndex++;
            if (m_currentIndex >= m_processableItems.Length)
            {
                if (m_info.onCompleted != null)
                {
                    m_info.onCompleted();
                }

                m_currentIndex = -1;
                return;
            }

            if (m_processableItems[m_currentIndex] == null)
            {
                UnityEngine.Debug.LogErrorFormat("m_processableItems[{0}] == null", m_currentIndex);
                RunGameCommands();
                return;
            }

            m_processableItems[m_currentIndex].caster = m_info.catser;
            m_processableItems[m_currentIndex].receiver = m_info.receiver;
            m_processableItems[m_currentIndex].Process(RunGameCommands);
        }
    }
}
