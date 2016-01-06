using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.geneticOptymalization
{
    class Generation
    {
        public int number { get; private set; }
        public List<Chromosom> chromosomes { get; set; }
        public Chromosom bestCh { get; set; }


        public Generation(int number, List<Chromosom> ch)
        {
            this.number = number;
            this.chromosomes = ch;
        }


    }
}
