using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace ZJ.App.Common
{
    [Serializable]
    public abstract class WhereClauseBuilder
    {
        protected  abstract char ParameterChar { get; }
    }
}
