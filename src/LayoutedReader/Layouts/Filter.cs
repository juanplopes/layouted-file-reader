using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using LayoutedReader.Filters;

namespace LayoutedReader.Layouts
{
    [XmlRoot("deploy")]
    public class Filter
    {
        string expression = null;
        IFilter filter = TrueFilter.Instance;

        [XmlAttribute("expr")]
        public string Expression
        {
            get { return expression; }
            set { expression = value; filter = new BooExpression(expression); }
        }

        [XmlElement("special")]
        public List<Filter> Special { get; set; }

        [XmlElement("filter")]
        public List<Filter> Ordinary { get; set; }

        [XmlElement("make")]
        public List<Action> Actions { get; set; }

        public Filter()
        {
            Special = new List<Filter>();
            Ordinary = new List<Filter>();
            Actions = new List<Action>();
        }

        public Filter(string expression) : this()
        {
            Expression = expression;
        }

        private bool AppliesTo(RecordContext context)
        {
            return filter.AppliesTo(context.Record, context.Header);
        }

        public IEnumerable<ValueBag> Evaluate(RecordContext context)
        {
            var mine = Actions.Select(x => x.Act(context));
            var special = Special.Where(x => x.AppliesTo(context)).ToList();
            if (special.Count > 0)
                return special.SelectMany(x => x.Evaluate(context))
                    .Union(mine);

            return Ordinary.Where(x => x.AppliesTo(context))
                .SelectMany(x => x.Evaluate(context))
                .Union(mine);
        }
    }
}
