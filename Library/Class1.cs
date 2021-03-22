using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Library
{
    public class Methods
    {

        private Dictionary<string, int> ToDict(string st)
        {
            Dictionary<string, int> book = new Dictionary<string, int>();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '<', '>', '\n', ')', '(', '!', '?', ';', '"', ']', '[', '/', '-' };//разделители
            string[] words = st.Split(delimiterChars).Select(word => word.ToLower()).ToArray();
            //Split - разбивает строку на подстроки            
            //ToLower - возвращает строку в нижнем регистре
            string[] uniquewords = words.Distinct().ToArray();
            //Distinct - возвращает уникальные строки
            foreach (string i in uniquewords)
            {
                int count = 0;
                count = words.Where(word => word.Equals(i)).Count();
                //подсчет одинаковых строк
                book.Add(i, count);
            }

            return book;
        }

        public static Dictionary<string, int> ToDictForeach(string st)
        {
            Dictionary<string, int> book = new Dictionary<string, int>();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '<', '>', '\n', ')', '(', '!', '?', ';', '"', ']', '[', '/', '-' };
            string[] words = st.Split(delimiterChars).Select(word => word.ToLower()).ToArray();

            string[] uniquewords = null;

            uniquewords = words.Distinct().ToArray();

            Parallel.ForEach(uniquewords, word =>
            {
                int count = 0;
                count = words.Where(ex => ex.Equals(word)).Count();
                book.Add(word, count);
            });//параллельная работа с элементами uniquewords
            return book;

        }

    }
}

