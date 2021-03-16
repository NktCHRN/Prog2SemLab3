using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextLib;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramInfo();
            Console.WriteLine("\nText");
            Text text1 = new Text();									//создаем экземпляр класса текст
            uint size;
            bool parsed;                                                //прошел ли успешно парсинг введенной строки
            do
            {
                Console.WriteLine("How many strings to you want to enter?");
                parsed = uint.TryParse(Console.ReadLine(), out size);
            }
            while (!parsed);
            Console.WriteLine("Enter the text: ");
            string str;
            for (int i = 0; i < size; i++)
            {
                str = Console.ReadLine();
                text1.AddString(in str);                                //добавляем введенные строки в текст
            }
            Console.WriteLine("\nYour text: \n");
            Console.WriteLine(text1.GetStringForPrint());		            //вывод текста
            int index;
            int repeat;
            do
            {
                do
                {
                    Console.WriteLine("Enter the index");
                    parsed = int.TryParse(Console.ReadLine(), out index);
                }
                while (!parsed);
                Console.WriteLine($"There are {text1[index]} elements at index {index}");       //проверяем, что вернул индексатор
                Console.WriteLine($"ErrorFlag is {text1.ErrorFlag}");                           //проверяем статус операции
                do
                {
                    Console.WriteLine("Do you want to enter another index?");
                    Console.WriteLine("0. No");
                    Console.WriteLine("1. Yes");
                    parsed = int.TryParse(Console.ReadLine(), out repeat);
                }
                while (!parsed || repeat < 0 || repeat > 1);
            } while (repeat != 0);
            Console.WriteLine($"There are {text1.VowelsQuantity} vowels in the text");          //получаем кол-во гласных из свойства
            Console.WriteLine("Press ENTER to exit");
            Console.ReadKey();                                          //дабы не завершилась программа
        }
        static void ProgramInfo()							            //информация о программе
        {
            Console.WriteLine("Lab N3. Nikita Chernikov, IS-02");
            Console.WriteLine("Researching of indexators and properties");
            Console.WriteLine("Variant 15");
        }
    }
}
