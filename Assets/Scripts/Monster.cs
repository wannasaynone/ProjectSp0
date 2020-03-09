namespace ProjectSP0
{
    public class Monster : ICombatUnit
    {
        public event System.Action<ICombatUnit> OnDied = null;

        public Int32ValueObject Distance { get; private set; }
        public Int32ValueObject HP { get; private set; }
        public int MinAttackDistance { get; private set; } 
        public int MaxAttackDistance { get; private set; }
        public AI.AIBehaviourProcesser AIBehaviour { get; private set; }

        public Manager.GameBuffManager GameBuffManager { get; private set; }

        private string m_name = "";
        private int m_defaultAttack = 0;
        private int m_defaultDefence = 0;
        private int m_defaultDex = 0;

        public Monster(MonsterRawData monsterData, int distance)
        {
            Distance = new Int32ValueObject(distance);
            HP = new Int32ValueObject(monsterData.DefaultHP);
            HP.OnValueChanged += OnHPChanged;
            m_defaultDefence = monsterData.DefaultDefence;
            m_defaultAttack = monsterData.DefaultAttack;
            m_defaultDex = monsterData.DefaultDex;
            MinAttackDistance = monsterData.DefaultMinAttackDistance;
            MaxAttackDistance = monsterData.DefaultMaxAttackDistance;
            m_name = monsterData.MonsterName;
            GameBuffManager = new Manager.GameBuffManager(this);
            if(monsterData.DefaultBuffs != null && monsterData.DefaultBuffs.Length != 0)
            {
                for(int i = 0; i < monsterData.DefaultBuffs.Length; i++)
                {
                    GameBuffManager.AddBuff(monsterData.DefaultBuffs[i]);
                }
            }
            AIBehaviour = new AI.AIBehaviourProcesser(this, monsterData.AIBehaviour);
        }

        ~Monster()
        {
            HP.OnValueChanged -= OnHPChanged;
        }

        public string GetName()
        {
            return m_name;
        }

        public int GetAttack()
        {
            return m_defaultAttack;
        }

        public int GetDefence()
        {
            return m_defaultDefence;
        }

        public int GetDex()
        {
            return m_defaultDex;
        }

        private void OnHPChanged(int value)
        {
            if (value <= 0)
            {
                OnDied?.Invoke(this);
            }
        }
    }
}
