namespace ProjectSP0.Manager
{
    public class PlayerManager : KahaGameCore.Interface.Manager
    {
        public static PlayerManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new PlayerManager();
                }
                return m_instance;
            }
        }
        private static PlayerManager m_instance = null;
        private PlayerManager() { }
    }
}
