using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LayoutedReader.Layouts
{
    public class Deployer
    {
        Layout layout;
        Filter filter;

        public Deployer(Layout layout)
            : this(layout, null)
        {
        }

        public Deployer(Layout layout, Filter filter)
        {
            this.layout = layout ?? new Layout();
            this.filter = filter ?? new Filter();
        }

        public FileContext<DeployContext> Read(Stream stream)
        {
            var enumerable = layout.Read(stream);
            var header = new DeployContext(enumerable.HeaderContext, new ValueBag[0], 0);

            return new FileContext<DeployContext>(header, ExpandRecords(enumerable));
        }

        private IEnumerable<DeployContext> ExpandRecords(IEnumerable<RecordContext> enumerable)
        {
            long expandedCount = 0;
            foreach (var ctx in enumerable)
            {
                var expanded = filter.Evaluate(ctx).ToArray();
                expandedCount += expanded.Length;

                var deployed = new DeployContext(ctx, expanded, expandedCount);
                yield return deployed;
            }
        }
    }
}
