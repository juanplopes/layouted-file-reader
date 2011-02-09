using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Layouts
{
    public partial class DeployContext
    {
        public DeployContext PrintInfo()
        {
            PrintInfo(Elapsed);
            return this;
        }

        public const string HeaderStringFormat = "{0,4} {1,8} {2,8} {3,8} {4,9} {5,7} {6,7}";
        public const string DefaultFormat = @"{0,4:0%} {1,8} {2,8} {3,8:0.0} {4,9:0.0} {5,7:0.0} {6,7:0.0}";

        public static void PrintHeader()
        {
            Console.WriteLine(HeaderStringFormat, "go", "count", "count+", "speed", "seek", "time", "eta");
        }

        public DeployContext PrintInfo(TimeSpan time)
        {
            return PrintInfo(DefaultFormat, time);
        }

        static TimeSpan lastPrinted = new TimeSpan();
        public DeployContext PrintInfo(string format, TimeSpan time)
        {
            if (time.TotalSeconds - lastPrinted.TotalSeconds > 0.1 || time < lastPrinted || StreamPosition == StreamSize)
            {
                int left = Console.CursorLeft;
                int top = Console.CursorTop;

                Console.WriteLine(format, 
                    PercentageDone,
                    Count, 
                    ExpandedCount,
                    RecordSpeed(time),
                    StreamPositionFormatted,
                    PrintTimespan(time),
                    PrintTimespan(EstimatedTime(time)));

                Console.CursorTop = top;
                Console.CursorLeft = left;
                lastPrinted = time;
            }
            return this;
        }

        private static string PrintTimespan(TimeSpan time)
        {
            return string.Format("{0:0}:{1:00}", time.TotalMinutes, time.Seconds);
        }
    }
}
