using KahaGameCore.Interface;

namespace ProjectSP0.UI
{
    public class MonsterIcon : View
    {
        public event System.Action<Monster> OnSelected = null;

        private Monster m_monster = null;

        public void LinkMonster(Monster monster)
        {
            m_monster = monster;
        }

        public void Button_Select()
        {
            OnSelected?.Invoke(m_monster);
        }
    }
}
