using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace MSI_cooperative_coevolution_v3
{
    class Program
    {
        static void Main(string[] args)
        {
            /////////////___USTAWIENIA___/////////////////////////////////////////////////////////////////////////////////
            int _ILOSC_WEKTOROW = 100;
            int _Enabile_vector_printing_after_modifications = 0; // 1 zeby drukowac, 0 zeby nie drukowac
            // zmienna c znajduje się w funckji "_CalculateFitness" w pliku non-generic-static.cs i tam nalezy ją edytować
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Random rng = new Random();
            
            Person _WZORZEC = new Person();
            _WZORZEC.tab_155._GenerateTab(rng);
            _WZORZEC.tab_60._GenerateTab(rng);
            _WZORZEC._CalculateFitness(_WZORZEC);
            _WZORZEC._PrintWzorzec();

            Person _bestOne = new Person();

            Person[] pop_1= new Person[_ILOSC_WEKTOROW];
            Person[] pop_1_new;
            
            //----------------------------------------------------------------

            //tworzenie populacji 1
            pop_1._MakePop(rng);
            pop_1._CalculatePopFitness(_WZORZEC);

            Console.WriteLine("----------------------------------------------------------------------------------------");
            pop_1._Crossowanie(rng);
            pop_1._PopSort();

            _bestOne._SaveBestOneFrom(pop_1);
            int licznik_Iteracji = 0;
            do
            {
                //zapisuje najlepszego który wystąpił
                if (_bestOne.fitness < pop_1[0].fitness)
                    _bestOne._SaveBestOneFrom(pop_1);


                //zostawia 155, generuje nowe 60
                Console.WriteLine($"\n----------operacja: {licznik_Iteracji++}: zostawia 155, generuje nowe 60;----------\n");
                
                pop_1_new = pop_1._SelectChildren();
                for (int i = 0; i < pop_1_new.Length; i++)
                {
                    pop_1_new[i].tab_155 = pop_1_new[0].tab_155;
                    pop_1_new[i].tab_60._GenerateTab(rng);
                }
                pop_1_new._CalculatePopFitness(_WZORZEC);
                pop_1_new._PopSort();

                pop_1 = pop_1_new;

                if (_Enabile_vector_printing_after_modifications == 1) pop_1._PopPrint();


                //zosatwia 60, generuje nowe 155
                
                Console.WriteLine($"\n----------operacja: {licznik_Iteracji++}: zosatwia 60, generuje nowe 155;----------\n");
                pop_1_new = pop_1._SelectChildren();
                for (int i = 0; i < pop_1_new.Length; i++)
                {
                    pop_1_new[i].tab_155._GenerateTab(rng); 
                    pop_1_new[i].tab_60 = pop_1_new[0].tab_60;
                }
                pop_1_new._CalculatePopFitness(_WZORZEC);
                pop_1_new._PopSort();

                pop_1 = pop_1_new;

                if (_Enabile_vector_printing_after_modifications == 1) pop_1._PopPrint();

            } while ( (pop_1[0].fitness > _bestOne.fitness ) && ( pop_1.Length>2 ) );

            _bestOne._WypiszNajlepszego();
            
            Console.Read();
            

        }
        
    }
    
}
