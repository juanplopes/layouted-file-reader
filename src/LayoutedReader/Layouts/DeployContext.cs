using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Layouts
{
    public partial class DeployContext : RecordContext
    {
        public IList<ValueBag> Expanded { get; private set; }
        public long ExpandedCount { get; private set; }

        public DeployContext(RecordContext context, IList<ValueBag> expanded, long expandedCount)
            : base(context.Row, context.Header, context.Record, context.Count, context.EstimatedTotal, context.StreamPosition, context.StreamSize, context.Elapsed)
        {
            this.Expanded = expanded;
            this.ExpandedCount = expandedCount;
        }
    }
}
