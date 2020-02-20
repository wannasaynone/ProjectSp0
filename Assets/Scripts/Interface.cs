namespace ProjectSP0
{
    public interface ICombatUnit
    {
        Int32ValueObject HP { get; }
        int GetAttack();
        int GetDefence();
        int GetDex();
        Manager.GameBuffManager GameBuffManager { get; }
    }
}