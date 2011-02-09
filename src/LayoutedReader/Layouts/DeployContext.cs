using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Layouts
{
    public partial class DeployContext : RecordContext
    {
        public IList<ValueBag> Expanded { get; private set; }
        public TimeSpan Elapsed { get; private set; }
        public string Filename { get; private set; }
        public long ExpandedCount { get; set; }

        public DeployContext(RecordContext context, string filename, IList<ValueBag> expanded, TimeSpan elapsed, long expandedCount)
            : base(context.Header, context.Record, context.Count, context.EstimatedTotal, context.StreamPosition, context.StreamSize)
        {
            this.Expanded = expanded;
            this.Elapsed = elapsed;
            this.Filename = filename;
            this.ExpandedCount = expandedCount;
        }
    }
}
