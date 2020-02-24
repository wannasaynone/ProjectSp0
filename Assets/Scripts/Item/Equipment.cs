using UnityEngine;

namespace ProjectSP0.Item
{
    [CreateAssetMenu(menuName = "Game Data/Item/Equipment")]
    public class Equipment : ScriptableObject
    {
        public enum ItemType
        {
            ArmsEquipment,
            HeadEquipment,
            BodyEquipment,
            FeetEquipment
        }

        public ItemType Type { get { return m_itemType; } }
        public int AddAttack { get { return m_addAttack; } }
        public int AddDefence { get { return m_addDefence; } }
        public int AddDex { get { return m_addDex; } }
        public int MinAttackDistance { get { return m_minAttackDistance; } }
        public int MaxAttackDistance { get { return m_maxAttackDistance; } }

        [SerializeField] private ItemType m_itemType = ItemType.ArmsEquipment;
        [SerializeField] private int m_addAttack = 0;
        [SerializeField] private int m_addDefence = 0;
        [SerializeField] private int m_addDex = 0;
        [SerializeField] private int m_minAttackDistance = 0;
        [SerializeField] private int m_maxAttackDistance = 1;
    }
}
