using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace ALGO
{
    public class Tri
    {

        public static void TriParSelection(string[] arr)
        {

            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (String.Compare(arr[j], arr[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }
                // Swap the minimum element with the current element
                string temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }
        }

        public static void TriParInsertion(string[] t)
        {
            int n = t.Length;
            for (int i = 1; i < n; i++)
            {
                string x = t[i];
                int j = i;
                while (j > 0 && string.Compare(t[j - 1], x) > 0)
                {
                    t[j] = t[j - 1];
                    j--;
                }
                t[j] = x;
            }
        }
        public static void TriBulle(string[] t)
        {
            int n = t.Length;
            for (int i = n - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (string.Compare(t[j + 1], t[j]) < 0)
                    {
                        string tmp = t[j];
                        t[j] = t[j + 1];
                        t[j + 1] = tmp;
                    }
                }
            }
        }

        static int Partitionner(string[] t, int premier, int dernier, int pivot)
        {
            Swap(ref t[pivot], ref t[dernier]);
            int j = premier;
            for (int i = premier; i < dernier; i++)
            {
                if (string.Compare(t[i], t[dernier]) <= 0)
                {
                    Swap(ref t[i], ref t[j]);
                    j++;
                }
            }
            Swap(ref t[dernier], ref t[j]);
            return j;
        }

        static void Swap(ref string a, ref string b)
        {
            string tmp = a;
            a = b;
            b = tmp;
        }

        public static void QuickSort(string[] t, int premier, int dernier)
        {
            if (premier < dernier)
            {
                Random rand = new Random();
                int pivot = rand.Next(premier, dernier + 1);
                pivot = Partitionner(t, premier, dernier, pivot);
                QuickSort(t, premier, pivot - 1);
                QuickSort(t, pivot + 1, dernier);
            }
        }
    }
}
