using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.IO;
using System.Threading;

namespace LayoutedReader.Layouts
{
    public class RecordContext
    {
        public ValueBag Header { get; private set; }
        public ValueBag Record { get; private set; }
        public long Count { get; private set; }
        public long StreamPosition { get; private set; }
        public long StreamSize { get; private set; }
        public long EstimatedTotal { get; set; }

        public double RecordSpeed(TimeSpan time)
        {
            return time.TotalSeconds > 0 ? Count / time.TotalSeconds : 0;
        }

        public double StreamSpeed(TimeSpan time)
        {
            return time.TotalSeconds > 0 ? StreamPosition / time.TotalSeconds : 0;
        }

        public TimeSpan EstimatedTime(TimeSpan time)
        {
            
            var speed = StreamSpeed(time);
            return speed > 0 ? TimeSpan.FromSeconds((StreamSize - StreamPosition) / speed) : TimeSpan.FromSeconds(600000);
        }

        public double PercentageDone
        {
            get { return StreamSize > 0 ? StreamPosition / (double)StreamSize : 1; }
        }
        public FileSize StreamPositionFormatted
        {
            get { return new FileSize(StreamPosition).InBestUnit(); }
        }
        public FileSize StreamSizeFormatted
        {
            get { return new FileSize(StreamSize).InBestUnit(); }
        }

        public RecordContext(ValueBag header, ValueBag record) : this(header, record, 0, 0, 0, 0) { }

        public RecordContext(ValueBag header, ValueBag record, long count, long estimatedTotal, long streamPosition, long streamSize)
        {
            this.Header = header ?? new ValueBag();
            this.Record = record ?? new ValueBag();
            this.Count = count;
            this.StreamPosition = streamPosition;
            this.StreamSize = streamSize;
            this.EstimatedTotal = estimatedTotal;
        }

        public static implicit operator RecordContext(ValueBag bag)
        {
            return new RecordContext(null, bag);
        }


    }
}
