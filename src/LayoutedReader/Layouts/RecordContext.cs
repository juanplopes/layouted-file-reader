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
        public TimeSpan Elapsed { get; private set; }
        public string Row { get; private set; }

        public double RecordSpeed
        {
            get { return Elapsed.TotalSeconds > 0 ? Count / Elapsed.TotalSeconds : 0; }
        }

        public double StreamSpeed
        {
            get { return Elapsed.TotalSeconds > 0 ? StreamPosition / Elapsed.TotalSeconds : 0; }
        }

        public TimeSpan EstimatedTime
        {
            get
            {
                var speed = StreamSpeed;
                return speed > 0 ? TimeSpan.FromSeconds((StreamSize - StreamPosition) / speed) : TimeSpan.FromSeconds(600000);
            }
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

        public RecordContext(ValueBag header, ValueBag record) : this("", header, record, 0, 0, 0, 0, TimeSpan.Zero) { }

        public RecordContext(string row, ValueBag header, ValueBag record, long count, long estimatedTotal, long streamPosition, long streamSize, TimeSpan elapsed)
        {
            this.Header = header ?? new ValueBag();
            this.Record = record ?? new ValueBag();
            this.Count = count;
            this.StreamPosition = streamPosition;
            this.StreamSize = streamSize;
            this.EstimatedTotal = estimatedTotal;
            this.Elapsed = elapsed;
            this.Row = row;
        }

        public static implicit operator RecordContext(ValueBag bag)
        {
            return new RecordContext(null, bag);
        }


    }
}
