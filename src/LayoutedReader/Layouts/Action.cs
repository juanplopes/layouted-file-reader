using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LayoutedReader.Layouts
{
    public class Action : Filter
    {
        [XmlAttribute("tipo")]
        public string TipoOperacao { get; set; }

        [XmlAttribute("parte")]
        public string ParteCobrada { get; set; }

        public Action() { }
        public Action(string tipo, string parte)
        {
            this.TipoOperacao = TipoOperacao;
            this.ParteCobrada = parte;
        }

        public ValueBag Act(RecordContext context)
        {
            var bag = new ValueBag(context.GetBags());
            bag.Set("tipoOperacao", TipoOperacao);
            bag.Set("parteCobrada", ParteCobrada);
            return bag;
        }
    }
}
