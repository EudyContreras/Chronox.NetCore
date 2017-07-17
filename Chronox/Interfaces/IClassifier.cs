
using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Interfaces
{
    internal interface IClassifier<TArg1,TArg2,TReturn>
    {
        TReturn Classify(TArg1 type1, TArg2 type2);
    }
}
