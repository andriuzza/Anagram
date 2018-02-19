using Anagrams.EFCF.GenericTask.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams.EFCF.GenericTask
{
    public class GenericAndEnum
    {
        public static Gender MapIntToGender(int value)
        {
            Gender result;
            if (!Enum.TryParse(value.ToString(), out result))
            {
                throw new Exception($"Value '{value}' is not part of Gender enum");
            }

            return result;
        }

        public static Gender MapStringToGender(string value)
        {
            Gender result;
            if (!Enum.TryParse(value, out result))
            {
                throw new Exception($"Value '{value}' is not part of Gender enum");
            }

            return result;
        }

        public static Weekday MapStringToWeekday(string value)
        {
            Weekday result;
            if (!Enum.TryParse(value, out result))
            {
                throw new Exception($"Value '{value}' is not part of Weekday enum");
            }

            return result;
        }

        public static T MapValueToEnum<T> (object value) where T : struct, IConvertible
        {
            T result;

            var strValue = value.ToString();
            Enum.TryParse(strValue, out result);

            return result;
        }

    }
}
