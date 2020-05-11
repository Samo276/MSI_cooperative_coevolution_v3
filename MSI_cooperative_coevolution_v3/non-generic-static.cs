using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSI_cooperative_coevolution_v3
{
    
    static class non_generic_static
    {
        
        public static void _GenerateTab(this int[] tab, Random rng)
        {
            //Random rng = new Random();
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = new int();
                tab[i] = rng.Next(0, 2);
            }
                
        }
        public static void _CalculateFitness(this Person porownywany, Person wzorzec)
        {
            double _ZMIENNA_C = 0.01;//<<<<----------------------------------------

            double licznik_60 = 0;
            double licznik_155 = 0;
            double licznik_215 = 0;

            
            for (int i = 0; i < wzorzec.tab_155.Length; i++)
                if (wzorzec.tab_155[i] == 0) licznik_155 += 1.0;
            for (int i = 0; i < wzorzec.tab_60.Length; i++)
                if (wzorzec.tab_60[i] == 0) licznik_60 += 1.0;


            for (int i = 0; i < wzorzec.tab_155.Length; i++)           
                if (wzorzec.tab_155[i] == porownywany.tab_155[i]) licznik_215 += 1.0;
            for (int i = 0; i < wzorzec.tab_60.Length; i++)
                if (wzorzec.tab_60[i] == porownywany.tab_60[i]) licznik_215 += 1.0;
                

            porownywany.fitness = (_ZMIENNA_C * licznik_155 * licznik_60) + ((1.0 - _ZMIENNA_C) * licznik_215);
        }
        public static void _PopSort(this Person[] pop)
        {
            Person temp;
            int smallest;
            for (int i = 0; i < pop.Length; i++)
            {
                smallest = i;
                for (int j = i + 1; j < pop.Length; j++)
                {
                    if (pop[j].fitness > pop[smallest].fitness)
                    {
                        smallest = j;
                    }
                }
                temp = pop[smallest];
                pop[smallest] = pop[i];
                pop[i] = temp;
            }
        }
        public static void _PopPrint(this Person[] pop)
        {
            for (int i = 0; i < pop.Length; i++)
            {
                pop[i]._Print();
            }
        }
        public static void _CalculatePopFitness(this Person[] pop, Person _wzorzec) 
        {
            for (int i = 0; i < pop.Length; i++)
            {
                pop[i]._CalculateFitness(_wzorzec);
                //pop[i]._Print();
            }
        }//wylicza fitness i wypisuje populacje
        public static void _Print(this Person osobnik_do_druku)
        {
           
            for (int i=0;i< osobnik_do_druku.tab_155.Length;i++) 
                Console.Write(osobnik_do_druku.tab_155[i]);
            
            for (int i=0;i< osobnik_do_druku.tab_60.Length;i++) 
                Console.Write(osobnik_do_druku.tab_60[i]);
            
            Console.WriteLine($"\nFitness względem wzorca: {osobnik_do_druku.fitness}\n");
        }
        public static void _MakePop(this Person[] pop, Random rng)
        {
            for (int i = 0; i < pop.Length; i++)
            {
                pop[i] = new Person();
                pop[i].tab_155._GenerateTab(rng);
                pop[i].tab_60._GenerateTab(rng);
            }
        }
        public static void _Crossowanie(this Person[] pop, Random rng)
        {
            //Random rng = new Random();
            Person tmp = new Person();
            int koniec = pop.Length;

            for (int i = 0; i < koniec; i++)
            {
                tmp.tab_60 = pop[rng.Next(0, koniec)].tab_60;
                pop[rng.Next(0, koniec)].tab_60 = pop[i].tab_60;
                pop[i].tab_60 = tmp.tab_60;
            }
        }
        public static void _SaveBestOneFrom (this Person _best_one, Person []_populacja )
        {
            for (int i = 0; i < _best_one.tab_155.Length; i++)
                _best_one.tab_155[i] = _populacja[0].tab_155[i];
            
            for (int i = 0; i < _best_one.tab_60.Length; i++)
                _best_one.tab_60[i] = _populacja[0].tab_60[i];

            _best_one.fitness = _populacja[0].fitness;
            
        }
        public static Person[] _SelectChildren(this Person[] pop)
        {
            Person[] new_Pop = new Person[(int)(pop.Length * 0.7)];

            for (int i = 0; i < new_Pop.Length; i++)
                new_Pop[i] = new Person();
            
            for (int k = 0; k < new_Pop.Length; k++)
            {
                for (int i = 0; i < new_Pop[k].tab_155.Length; i++)
                       new_Pop[k].tab_155[i] = pop[k].tab_155[i];
                
                for (int i = 0; i < new_Pop[k].tab_60.Length; i++)
                    new_Pop[k].tab_60[i] = pop[k].tab_60[i];
            }

            return new_Pop;
        }
        public static void _WypiszNajlepszego(this Person _bestOne)
        {
            Console.WriteLine("----------------------------------------------------------------------------------------\nNajlepszy znaleziony wektor:\n");
            
            for (int i = 0; i < _bestOne.tab_155.Length; i++)
                Console.Write( _bestOne.tab_155[i]);
            for (int i = 0; i < _bestOne.tab_60.Length; i++)
                Console.Write(_bestOne.tab_60[i]);

            Console.WriteLine($"\nFitness względem wzorca: {_bestOne.fitness}\n----------------------------------------------------------------------------------------");
        }
        public static void _PrintWzorzec(this Person osobnik_do_druku)
        {
            Console.WriteLine("Wzorzec:");
            for (int i = 0; i < osobnik_do_druku.tab_155.Length; i++)
                Console.Write(osobnik_do_druku.tab_155[i]);

            for (int i = 0; i < osobnik_do_druku.tab_60.Length; i++)
                Console.Write(osobnik_do_druku.tab_60[i]);

            Console.WriteLine($"\nFitness Wzorca: {osobnik_do_druku.fitness}\n");
        }
    }
}
