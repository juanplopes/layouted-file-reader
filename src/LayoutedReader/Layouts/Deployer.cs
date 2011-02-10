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

        public IEnumerable<DeployContext> Read(Stream stream)
        {
            long expandedCount = 0;
            foreach (var ctx in layout.Read(stream))
            {
                var expanded = filter.Evaluate(ctx).ToArray();
                expandedCount += expanded.Length;
              
                var deployed = new DeployContext(ctx, expanded, expandedCount);
                yield return deployed;
            }
        }
    }
}
