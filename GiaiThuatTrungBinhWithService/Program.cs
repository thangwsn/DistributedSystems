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
            String get_result = service.getInputData("thangnd", "1", 1, 2, ref input);
            Console.WriteLine(get_result);
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine(input[i]);
            }
            string[] output = GiaiThuatTrungBinh(input);
            String result = service.submit("thangnd", "1", 1, 2, output);
            Console.WriteLine(result);

            for (int i = 0; i < output.Length; i++)
            {
                Console.WriteLine(output[i]);
            }
            Console.ReadKey();
        }

        static string[] GiaiThuatTrungBinh(string[] input)
        {
            string[] result = new string[input.Length];
            string pre_str = input[0].Trim().Split(' ')[0];

            int n = input.Length;
            int[] timeToMsArr = new int[n];

            // convert time to ms
            for (int i = 0; i < n; i++)
            {
                string time_str = input[i].Trim().Split(' ')[1];
                // 2021-06-30 01:01:42.010
                timeToMsArr[i] = Int32.Parse(time_str.Substring(0, 2)) * 60 * 60 * 1000
                                + Int32.Parse(time_str.Substring(3, 2)) * 60 * 1000
                                + Int32.Parse(time_str.Substring(6, 2)) * 1000
                                + Int32.Parse(time_str.Substring(9, 3));
            }

            // calc
            List<int> newTimeMsArr = new List<int>();
            int[] result_ms = new int[n];
            for (int i = 0; i < n; i++)
            {
                newTimeMsArr.Clear();
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        newTimeMsArr.Add(timeToMsArr[j]);
                    }
                }
                newTimeMsArr.Sort();
                newTimeMsArr.RemoveAt(0);
                newTimeMsArr.RemoveAt(newTimeMsArr.Count - 1);

                double sum = 0;
                int avg = 0;

                for (int k = 0; k < newTimeMsArr.Count; k++)
                {
                    sum += newTimeMsArr[k];
                }

                avg = (int) Math.Round(sum/ newTimeMsArr.Count, MidpointRounding.AwayFromZero);
                result_ms[i] = avg;
            }

            for (int i = 0; i < n; i++)
            {
                int hour = result_ms[i] / (60 * 60 * 1000);
                int min = result_ms[i] % (60 * 60 * 1000) / (60 * 1000);
                int sec = result_ms[i] % (60 * 1000) / 1000;
                int ms = result_ms[i] % 1000;

                string res = pre_str + " "
                            + (hour < 10 ? ("0" + hour.ToString()) : hour.ToString()) + ":"
                            + (min < 10 ? ("0" + min.ToString()) : min.ToString()) + ":"
                            + (sec < 10 ? ("0" + sec.ToString()) : sec.ToString()) + "."
                            + (ms < 10 ? ("00" + ms.ToString()) : (ms < 100 ? ("0"+ ms.ToString()) : ms.ToString() ));
                result[i] = res;
            }
            return result;
        }    
    }
}
