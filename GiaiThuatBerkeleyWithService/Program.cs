using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaiThuatBerkeleyWithService
{
    class Program
    {
        static void Main(string[] args)
        {
            ExamForBerkeley service = new ExamForBerkeley();
            string[] input;
            //get data
            string get_data = service.getInputData("thangnd", "1", 1, 1, out input);
            Console.WriteLine(get_data);
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine(input[i]);
            }
            int n = input.Length;
            //convert string to datetime
            string format = "yyyy-MM-dd HH:mm:ss.fff";
            DateTime[] inputDateTime = new DateTime[n];
            for (int i = 0; i < n; i++)
            {
                inputDateTime[i] = DateTime.Parse(input[i]);
            }
            // calc sum of all (difference vs coordiante, = member - coordinate)
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += (inputDateTime[i].Ticks - inputDateTime[0].Ticks) / 10000;
            }
            // calc average from differ array
            int avg = (int) Math.Round(sum * 1.0/ n, 0, MidpointRounding.AwayFromZero);
            // final time  
            DateTime correctedDateTime = inputDateTime[0].AddMilliseconds(avg);
            //control
            int[] result = new int[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = (int)((correctedDateTime.Ticks - inputDateTime[i].Ticks) / 10000);
            }
            //submit
            string point = service.submit("thangnd", "1", 1, 1, result, correctedDateTime.ToString(format));
            Console.WriteLine(point);
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine(result[i]);
            }
            Console.WriteLine(correctedDateTime);
            Console.ReadKey();
        }
    }
}
