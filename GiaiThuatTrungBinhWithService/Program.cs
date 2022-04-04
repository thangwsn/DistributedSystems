using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaiThuatTrungBinhWithService
{
    class Program
    {
        static void Main(string[] args)
        {
            ExamForTrungBinh service = new ExamForTrungBinh();
            string[] input = new string[13];
            //get data
            String get_result = service.getInputData("anonymous", "anonymous", 1, 7, ref input);
            Console.WriteLine(get_result);
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine(input[i]);
            }
            string[] output = GiaiThuatTrungBinh(input);
            //submit
            String result = service.submit("anonymous", "anonymous", 1, 7, output);
            Console.WriteLine(result);

            for (int i = 0; i < output.Length; i++)
            {
                Console.WriteLine(output[i]);
            }
            Console.ReadKey();
        }

        static string[] GiaiThuatTrungBinh(string[] input)
        {
            string format = "yyyy-MM-dd HH:mm:ss.fff";

            int n = input.Length;
            List<DateTime> inputDateTime = new List<DateTime>();

            // convert string to datetime
            for (int i = 0; i < n; i++)
            {
                DateTime dateTime = DateTime.Parse(input[i]);
                inputDateTime.Add(dateTime);
            }

            // calc
            List<DateTime> newDateTime = new List<DateTime>();
            DateTime[] result_date = new DateTime[n];
            for (int i = 0; i < n; i++)
            {
                newDateTime.Clear();
                for (int j = 0; j < n; j++)
                {
                    // add \ {self}
                    if (j != i)
                    {
                        newDateTime.Add(inputDateTime[j]);
                    }
                }
                newDateTime.Sort();
                // remove max, min
                newDateTime.RemoveAt(0);
                newDateTime.RemoveAt(newDateTime.Count - 1);

                double sum = 0;
                long avg = 0;
                //cald sum
                for (int k = 0; k < newDateTime.Count; k++)
                {
                    sum += newDateTime[k].Ticks;
                }
                //calc average
                avg = (long) Math.Round(sum / (newDateTime.Count*10000), 0, MidpointRounding.AwayFromZero);
                result_date[i] = new DateTime(avg*10000);
            }
            string[] result = new string[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = result_date[i].ToString(format);
            }
            return result;
        }    
    }
}
