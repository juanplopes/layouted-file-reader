using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using Simple.Reflection;

namespace LayoutedReader.Filters
{
    public class BooExpression : IFilter
    {
        Func<ValueBag[], bool> evaluator;

        static BooExpression()
        {

        }

        public BooExpression(string expression)
        {
            var script = string.Format(Scripts.Boo, expression);

            var booC = new BooCompiler();
            booC.Parameters.Input.Add(new StringInput("test", script));
            booC.Parameters.Pipeline = new CompileToMemory();
            var compiler = booC.Run();
            if (compiler.Errors.Count > 0)
                throw compiler.Errors[0];

            var invoker = MethodCache.Do.GetInvoker(
                compiler.GeneratedAssembly.GetType("TestModule").GetMethod("evaluate"));

            evaluator = x => (bool)(invoker(null, new ValueBagFu(x)) ?? false);
        }

        public bool AppliesTo(params ValueBag[] bags)
        {
            return evaluator(bags);
        }
    }
}
