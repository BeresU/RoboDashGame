namespace RoboDash.Attack.Extensions
{
    public static class AttackTypeExtensions
    {
        public static bool IsDefending(this AttackType type) => type == AttackType.Defence ||
                                                                type == AttackType.ReflectHigh ||
                                                                type == AttackType.ReflectLow;

        public static bool IsPunching(this AttackType type) => type == AttackType.Punch;
    }
}