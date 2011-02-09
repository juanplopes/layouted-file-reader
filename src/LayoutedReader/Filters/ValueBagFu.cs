using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Boo.Lang;

namespace LayoutedReader.Filters
{
    public class ValueBagFu : IQuackFu
    {
        public ValueBag[] Bags { get; private set; }
        #region IQuackFu Members

        public ValueBagFu(params ValueBag[] bags)
        {
            this.Bags = bags;
        }

        public object QuackGet(string name, object[] parameters)
        {
            ValueItem item;
            foreach(var bag in Bags)
                if (bag.TryGetValue(name, out item)) return item.Value;

            return null;
        }

        public object QuackInvoke(string name, params object[] args)
        {
            throw new NotImplementedException();
        }

        public object QuackSet(string name, object[] parameters, object value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
