using System;
using System.Collections.Generic;
using System.Linq;

namespace Arbiter.Core.Models
{
    public class Option
    {
        public string Label { get; set; }
        public int Value { get; set; }

        public static IEnumerable<Option> GetFromEnum<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return Enum.GetValues(typeof(T)).Cast<IConvertible>().Select(x => new Option()
            {
                Label = x.ToString(),
                Value = (int)x
            });
        }

        public static IEnumerable<Option> GetFromEnum<T>(IEnumerable<T> options) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return options.Cast<IConvertible>().Select(x => new Option()
            {       
                Label = x.ToString(),
                Value = (int)x
            });
        }
    }
}
