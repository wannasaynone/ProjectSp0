namespace ProjectSP0
{
    public interface ICombatUnit
    {
        Int32ValueObject HP { get; }
        string GetName();
        int GetAttack();
        int GetDefence();
        int GetDex();
        Manager.GameBuffManager GameBuffManager { get; }

        event System.Action<ICombatUnit> OnDied;
    }
}