using System;

namespace ProjectSP0
{
    public class Int32ValueObject
    {
        public int Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                OnValueChanged?.Invoke(m_value);
            }
        }
        private int m_value = 0;
        public event Action<int> OnValueChanged = default;

        public Int32ValueObject() { }
        public Int32ValueObject(int v) { m_value = v; }
    }
}
