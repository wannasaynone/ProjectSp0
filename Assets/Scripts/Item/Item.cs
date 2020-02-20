using UnityEngine;

namespace ProjectSP0.Item
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField] private GameBuff[] m_effects = null;

        public abstract void Use();
    }
}
