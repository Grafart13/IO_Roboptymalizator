using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.geneticOptymalization
{
    class Generation
    {
        // dane pokolenie
        public int number { get; private set; }
        public List<Chromosom> chromosomes { get; set; }
        
        public Generation(int number, List<Chromosom> ch)
        {
            this.number = number; // num of chromosoms
            this.chromosomes = ch;
        }


    }
}
