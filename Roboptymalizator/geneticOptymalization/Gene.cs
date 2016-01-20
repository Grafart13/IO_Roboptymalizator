using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roboptymalizator.geneticOptymalization
{
    class Gene
    {
        public Tuple<int,int> value { get; set; }

        public Gene(Tuple<int, int> _value)
        {
            value = _value;
        }

        public static bool operator ==(Gene a, Gene b)
        {
            return (a.value.Item1 == b.value.Item1) && (a.value.Item2 == b.value.Item2);
        }

        public static bool operator !=(Gene a, Gene b)
        {
            return ! (a == b);
        }

        public bool Equals(Gene b)
        {
            return this == b;
        }
    }
}
