using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiralArray
{
    class Program
    {

        static int[,] FillSpiral(int length, bool right = true, int startNum = 0)
        {
            if (length == 0)
                return new int[0, 0]; // {{startNum}};
            
            int[,] array = new int[length, length];

            for (int i = 0; i < length; i++)
                array[0, i] = startNum++;
            for (int i = 1; i < length; i++)
                array[i, length - 1] = startNum++;

            if (!right)
            {
                for (int i = 0; i < length/2; i++)
                    for (int j = 0; j < length; j++)
                    {
                        int temp = array[i, j];
                        array[i, j] = array[length - 1 - i, j];
                        array[length - 1 - i, j] = temp;
                    }
                for (int i = 0; i < length; i++)
                    for (int j = 0; j < length/2; j++)
                    {
                        int temp = array[i, j];
                        array[i, j] = array[i, length - 1 - j];
                        array[i, length - 1 - j] = temp;
                    }
            }

            int[,] smallArray = FillSpiral(length - 1, !right, startNum);

            if (!right)
                for (int i = 0; i < length - 1; i++)
                    for (int j = 1; j < length; j++)
                        array[i, j] = smallArray[i, j - 1];
            else
                for (int i = 1; i < length; i++)
                    for (int j = 0; j < length - 1; j++)
                        array[i, j] = smallArray[i - 1, j];

            return array;
        }

        static void FillSpiral2(int[,] array, ref int startNum, int xS, int yS, int xE, int yE, bool reverse = false)
        {
            if(xS == xE || yS == yE)
                return;

            int x = reverse ? xE : xS;
            do
            {
                int y = reverse ? yE : yS;
                do
                {
                    array[x, y] = startNum++;
                    y = reverse ? y - 1 : y + 1;

                } while (!(yE < y || yS > y));

                x = reverse ? x - 1 : x + 1;
            } while (!(xE < x || xS > x));
        }

        static void FillSpiral3(int[,] array, int x0, int y0, int xN, int yN, bool reverse = false, int startNum = 0)
        {
            do
            {
                FillSpiral2(array, ref startNum, x0, y0, x0++, yN, reverse);
                FillSpiral2(array, ref startNum, x0, yN, xN, yN, reverse);
                reverse = !reverse;
                FillSpiral2(array, ref startNum, xN, y0, xN, yN--, reverse);
                FillSpiral2(array, ref startNum, x0, y0, xN--, y0++, reverse);
            } while (x0 <= xN || y0 <= yN);
        }

        static void Main(string[] args)
        {
            const int N = 7;
            const int M = N;
            int[,] array = new int[N,M];

            array = FillSpiral(N, startNum:1);
            //int SN = 1;
            //FillSpiral3(array,0,0,6,6,true,1);

            Console.WriteLine();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write("{0,4}",array[i,j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey(true);
        }
    }
}
