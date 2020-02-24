using System;
using KahaGameCore.Common;
using ProjectSP0.GameCommand;
using ProjectSP0.Manager;

namespace ProjectSP0
{
    public class GameBuffContainer
    {
        public class BuffInfo
        {
            public GameBuff buff = null;
            public int remainingTime = 0;
            public int layCount = 1;
            public ICombatUnit owner = null;
        }

        public event Action<GameBuffContainer> OnBuffEffectPreStart = null;
        public event Action<GameBuffContainer> OnBuffEffectEnded = null;
        public event Action<GameBuffContainer> OnBuffTimeUp = null;

        public readonly BuffInfo info = null;

        public bool Actived { get; private set; }

        public GameBuffContainer(BuffInfo info)
        {
            this.info = info;
        }

        private Processer<GameCommandBase> m_processer = null;

        public void Active()
        {
            if(info.buff == null)
            {
                UnityEngine.Debug.LogError("[GameBuffContainer] info.buff == null");
                return;
            }

            GameManager.Instance.OnMinutesPassed += OnTimePassed;
            switch (info.buff.TriggerTiming)
            {
                case GameBuff.TriggerWhen.Immediate:
                    {
                        ProcessCommands();
                        break;
                    }
                case GameBuff.TriggerWhen.OnAttackPreSatrt:
                    {
                        CombatManager.Instance.OnAttackPreStart += ProcessCommands;
                        break;
                    }
                case GameBuff.TriggerWhen.OnAttacked:
                    {
                        CombatManager.Instance.OnAttacked += ProcessCommands;
                        break;
                    }
                case GameBuff.TriggerWhen.OnCombatEnded:
                    {
                        CombatManager.Instance.OnCombatEnded += ProcessCommands;
                        break;
                    }
                case GameBuff.TriggerWhen.OnCombatPreStart:
                    {
                        CombatManager.Instance.OnCombatEnded += ProcessCommands;
                        break;
                    }
                case GameBuff.TriggerWhen.OnDamaged:
                    {
                        break;
                    }
                case GameBuff.TriggerWhen.OnDied:
                    {
                        break;
                    }
                case GameBuff.TriggerWhen.OnTurnEnded:
                    {
                        CombatManager.Instance.OnTurnEnded += ProcessCommands;
                        break;
                    }
                case GameBuff.TriggerWhen.OnTurnPreStart:
                    {
                        CombatManager.Instance.OnTurnPreStart += ProcessCommands;
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException(info.buff.TriggerTiming.ToString());
                    }
            }

            Actived = true;
        }

        public void Deactive()
        {
            GameManager.Instance.OnMinutesPassed -= OnTimePassed;
            switch (info.buff.TriggerTiming)
            {
                case GameBuff.TriggerWhen.Immediate:
                    {
                        break;
                    }
                case GameBuff.TriggerWhen.OnAttackPreSatrt:
                    {
                        CombatManager.Instance.OnAttackPreStart -= ProcessCommands;
                        break;
                    }
                case GameBuff.TriggerWhen.OnAttacked:
                    {
                        CombatManager.Instance.OnAttacked -= ProcessCommands;
                        break;
                    }
                case GameBuff.TriggerWhen.OnCombatEnded:
                    {
                        CombatManager.Instance.OnCombatEnded -= ProcessCommands;
                        break;
                    }
                case GameBuff.TriggerWhen.OnCombatPreStart:
                    {
                        CombatManager.Instance.OnCombatEnded -= ProcessCommands;
                        break;
                    }
                case GameBuff.TriggerWhen.OnDamaged:
                    {
                        break;
                    }
                case GameBuff.TriggerWhen.OnDied:
                    {
                        break;
                    }
                case GameBuff.TriggerWhen.OnTurnEnded:
                    {
                        CombatManager.Instance.OnTurnEnded -= ProcessCommands;
                        break;
                    }
                case GameBuff.TriggerWhen.OnTurnPreStart:
                    {
                        CombatManager.Instance.OnTurnPreStart -= ProcessCommands;
                        break;
                    }
                default:
                    {
                        throw new System.NotImplementedException(info.buff.TriggerTiming.ToString());
                    }
            }

            Actived = false;
        }

        ~GameBuffContainer()
        {
            Deactive();
        }

        private void ProcessCommands(ICombatUnit unit)
        {
            if(unit == info.owner)
            {
                ProcessCommands();
            }
        }

        private int m_currentProcesserTimes = 0;
        private void ProcessCommands()
        {
            if (m_processer == null)
            {
                m_processer = new Processer<GameCommandBase>(info.buff.Commands);
            }

            if(info.remainingTime < 0 || info.layCount <= 0)
            {
                OnBuffEffectEnded?.Invoke(this);
                return;
            }

            OnBuffEffectPreStart?.Invoke(this);

            m_currentProcesserTimes = 0;
            m_processer.Start(OnBuffEnded);
        }

        private void OnBuffEnded()
        {
            m_currentProcesserTimes++;
            if (m_currentProcesserTimes >= info.layCount
                || info.remainingTime < 0
                || info.layCount <= 0)
            {
                OnBuffEffectEnded?.Invoke(this);
            }
            else
            {
                m_processer.Start(OnBuffEnded);
            }
        }

        private void OnTimePassed(int minutes)
        {
            info.remainingTime -= minutes;
            if(info.remainingTime <= 0)
            {
                OnBuffTimeUp?.Invoke(this);
            }
        }
    }
}

