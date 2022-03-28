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
            string get_data = service.getInputData("thangnd", "1", 1, 1, out input);
            Console.WriteLine(get_data);
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine(input[i]);
            }

            int[] differ_vs_coordinate = new int[input.Length];
            double sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                differ_vs_coordinate[i] = differ(input[i], input[0]);
                sum += differ_vs_coordinate[i];
            }
            int avg = (int) Math.Round(sum / differ_vs_coordinate.Length, MidpointRounding.AwayFromZero);
            // control
            int[] result = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = avg - differ_vs_coordinate[i];
            }
            int finalTime_ms = TimeToMiliSecond(input[0]) + result[0];
            // extract to final time
            int hour = finalTime_ms / (60 * 60 * 1000);
            int min = finalTime_ms % (60 * 60 * 1000) / (60 * 1000);
            int sec = finalTime_ms % (60 * 1000) / 1000;
            int ms = finalTime_ms % 1000;
            string correctedDateTime = input[0].Trim().Split(' ')[0] + " "
                                    + (hour < 10 ? ("0" + hour.ToString()) : hour.ToString()) + ":"
                                    + (min < 10 ? ("0" + min.ToString()) : min.ToString()) + ":"
                                    + (sec < 10 ? ("0" + sec.ToString()) : sec.ToString()) + "."
                                    + (ms < 10 ? ("00" + ms.ToString()) : (ms < 100 ? ("0" + ms.ToString()) : ms.ToString()));
            string point = service.submit("thangnd", "1", 1, 1, result, correctedDateTime);
            
            Console.WriteLine(point);
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine(result[i]);
            }
            Console.WriteLine(correctedDateTime);
            Console.ReadKey();
        }

        private static int differ(string t1, string t2)
        {
            int time1 = TimeToMiliSecond(t1);
            int time2 = TimeToMiliSecond(t2);
            return time1 - time2;
        }  
        
        private static int TimeToMiliSecond(string t1)
        {
            string time_str_1 = t1.Trim().Split(' ')[1];
            //07:55:02.239
            int time1 = Int32.Parse(time_str_1.Substring(0, 2)) * 60 * 60 * 1000
                        + Int32.Parse(time_str_1.Substring(3, 2)) * 60 * 1000
                        + Int32.Parse(time_str_1.Substring(6, 2)) * 1000
                        + Int32.Parse(time_str_1.Substring(9, 3));
            return time1;
        }
    }
}
