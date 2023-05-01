using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// LINQ запрос и все.

namespace Algorithm
{
    public partial class Algorithm
    {
        public static void MostCommonElement(List<int> numbers)
        {
            var most = numbers.GroupBy(x => x).OrderByDescending(x => x.Count()).First();         //находим самое частовстречаемое число
            Console.WriteLine("Наиболее часто встречается {0} в количестве {1}", most.Key, most.Count());       //Выводим его и число повторов;  Сначала группировка (GroupBy) по значениям элементов в группы, потом упорядочивание (OrderByDecsending) по количеству элементов в группе и дальше из массива групп элементов берется первая группа (First). 
        }
    }
}
