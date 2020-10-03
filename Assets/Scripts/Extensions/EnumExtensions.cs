using System;

namespace Extensions
{
    public static class EnumExtensions
    {
        public static TEnum[] GetValues<TEnum>(this TEnum @enum) where TEnum : Enum =>
            (TEnum[]) Enum.GetValues(typeof(TEnum));
    }
}