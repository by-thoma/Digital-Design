using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Library;
using System.Diagnostics;


namespace Тестовое
{
    class Program
    {


        static void Main(string[] args)
        {


            string filename = @"C:/Users/toma0/OneDrive/Рабочий стол/Война и мир.fb2";
            string sr = File.ReadAllText(filename, Encoding.UTF8);
            if (sr != null)//проверка на пустой файл
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Type type = typeof(Methods);//получаем ссылку на объект, содержащий информацию о Class1
                ConstructorInfo ctor = type.GetConstructor(new Type[] { });//поиск конструктора
                var result = ctor.Invoke(new object[] { });//вызов конструктора
                var Book = (Dictionary<string, int>)type.GetMethod("ToDict", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(result, new object[] { sr });//вызов приватного метода ToDict
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;//время, затраченное на выполнение части программы
                string elapsedTime = String.Format("{0:00}", ts.Seconds);//формат
                Console.WriteLine("Private method: " + elapsedTime + "sec");


                stopWatch.Restart();
                Methods.ToDictForeach(sr);
                stopWatch.Stop();
                ts = stopWatch.Elapsed;
                elapsedTime = String.Format("{0:00}", ts.Seconds);
                Console.WriteLine("Public method: " + elapsedTime + "sec");

                using StreamWriter wordsintext = new StreamWriter(@"C:/Users/toma0/OneDrive/Рабочий стол/WordsInText.txt");
                {
                    foreach (var i in Book.OrderByDescending(x => x.Value))
                    {
                        wordsintext.WriteLine($"{i.Key} - {i.Value}");
                    }//Вывод словаря в текстовый файл
                }
            }
            else
            {
                Console.WriteLine("Пустой документ");
            }
        }
    }

}