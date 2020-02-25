namespace ProjectSP0.Combat
{
    public abstract class CombatStateBase : KahaGameCore.Interface.Manager
    {
        public int CurrentAP { get; protected set; }
        public abstract void StartTurn(System.Action onTurnEnded);
        public abstract void Move(int distance);
        public abstract void Attack(ICombatUnit target, System.Action onAttackEnded);
    }
}
