using ProjectSP0.Item;

namespace ProjectSP0
{
    public class Character : ICombatUnit
    {
        public Int32ValueObject HP { get; private set; }
        public Equipment arms = null;
        public Equipment head = null;
        public Equipment body = null;
        public Equipment feet = null;

        public Manager.GameBuffManager GameBuffManager { get; private set; }

        private readonly string m_characterName = "";

        public Character(string name)
        {
            HP = new Int32ValueObject();
            GameBuffManager = new Manager.GameBuffManager(this);
            m_characterName = name;
        }

        public Character(Character from)
        {
            HP = new Int32ValueObject(from.HP.Value);
            arms = from.arms;
            head = from.head;
            feet = from.feet;
            body = from.body;
            GameBuffManager = new Manager.GameBuffManager(this);
        }

        public string GetName()
        {
            return m_characterName;
        }

        public int GetAttack()
        {
            int _value = 0;
            if(arms != null)
            {
                _value += arms.AddAttack;
            }
            if(head != null)
            {
                _value += head.AddAttack;
            }
            if(body != null)
            {
                _value += body.AddAttack;
            }
            if(feet != null)
            {
                _value += feet.AddAttack;
            }

            return _value <= 0 ? GameProperties.Instance.BaseAttack : _value;
        }

        public int GetDefence()
        {
            int _value = GameProperties.Instance.BaseDefence;
            if (arms != null)
            {
                _value += arms.AddDefence;
            }
            if (head != null)
            {
                _value += head.AddDefence;
            }
            if (body != null)
            {
                _value += body.AddDefence;
            }
            if (feet != null)
            {
                _value += feet.AddDefence;
            }

            return _value;
        }

        public int GetDex()
        {
            int _value = GameProperties.Instance.BaseDex;
            if (arms != null)
            {
                _value += arms.AddDex;
            }
            if (head != null)
            {
                _value += head.AddDex;
            }
            if (body != null)
            {
                _value += body.AddDex;
            }
            if (feet != null)
            {
                _value += feet.AddDex;
            }

            return _value;
        }

        public int GetMinAttackDistance()
        {
            return arms == null ? 0 : arms.MinAttackDistance;
        }

        public int GetMaxAttackDistance()
        {
            return arms == null ? 1 : arms.MaxAttackDistance;
        }
    }
}
