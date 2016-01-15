using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GALib;

namespace Roboptymalizator.geneticOptymalization
{
    class GeneticOpt
    {
     // private Population<>[] populations;

        // tworzymy losową populację złożoną z N osobników populacji
        // dopisujemy do niej M osobników potomnych, tworzonych na podstawie losowo wybranych osobników z poprzedniego pokolenia za pomocą mutacji i krzyżowania
        // dla każdego osobnika w populacji pośredniej liczymy wartość optymalizowanej funkcji
        // spośród N+M osobników wybieramy N najlepszych względem f(x). te przeżyją i utworzą następne pokolenie
        // powtarzamy od punktu 2 z aktualną populacją N-osobnikową

        // kod genetyczny osobnika = (x, fi) x - punkt, fi - wartość dodatnia rzeczywista

        private FitnessService fitnessService;
        private Population population;
        public GeneticOpt (heart.TerrainMap tm, heart.Robot rob)
        {
            fitnessService = new FitnessService(tm, rob);
            
            population = new Population(10, 3, 5, fitnessService);

        }

    }
}
