using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roboptymalizator.geneticOptymalization
{
    class Gene
    {
        public int value { get; set; }

        public Gene(int _value)
        {
            value = _value;
        }

        public static bool operator ==(Gene a, Gene b)
        {
            return a.value == b.value;
        }

        public static bool operator !=(Gene a, Gene b)
        {
            return a.value != b.value;
        }

        public bool Equals(Gene b)
        {
            return this == b;
        }
    }
}
