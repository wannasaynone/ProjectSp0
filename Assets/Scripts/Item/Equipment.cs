using UnityEngine;

namespace ProjectSP0.Item
{
    [CreateAssetMenu(menuName = "Game Data/Item/Equipment")]
    public class Equipment : Item
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

        [SerializeField] private ItemType m_itemType = ItemType.ArmsEquipment;
        [SerializeField] private int m_addAttack = 0;
        [SerializeField] private int m_addDefence = 0;
        [SerializeField] private int m_addDex = 0;

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
