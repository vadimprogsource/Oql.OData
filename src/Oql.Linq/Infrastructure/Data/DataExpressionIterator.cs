using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Data
{
    public abstract class DataExpressionIterator : IExpression
    {
        public virtual bool Add
        {
            get
            {
                return false;
            }
        }

        public virtual bool And
        {
            get
            {
                return true;
            }
        }

        public virtual bool Asc
        {
            get
            {
                return true;
            }
        }

        public virtual bool Desc
        {
            get
            {
                return false;
            }
        }

        public virtual bool Div
        {
            get
            {
                return false;
            }
        }

        public virtual bool Eq
        {
            get
            {
                return true;
            }
        }

        public virtual IExpression Expression
        {
            get
            {
                if (MoveNext())
                {
                    return this;
                }

                return null;
            }
        }

        private bool MoveNext()
        {
            return false;
        }

        public virtual bool Ge
        {
            get
            {
                return false;
            }
        }

        public virtual bool Gt
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsMethodCall
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsOf
        {
            get
            {
                return false;
            }
        }

        public virtual bool Le
        {
            get
            {
                return false;
            }
        }

        public virtual bool Lt
        {
            get
            {
                return false;
            }
        }

        public virtual bool Mod
        {
            get
            {
                return false;
            }
        }

        public virtual bool Mul
        {
            get
            {
                return false;
            }
        }

        public virtual string Name
        {
            get
            {
                 return string.Empty;
            }
        }

        public virtual bool Ne
        {
            get
            {
                return false;
            }
        }

        public virtual bool Negate
        {
            get
            {
                return false;
            }
        }

        public virtual bool Not
        {
            get
            {
                return false;
            }
        }

        public virtual bool Or
        {
            get
            {
                return false;
            }
        }

        public virtual bool Sub
        {
            get
            {
                return false;
            }
        }

        public virtual object Value
        {
            get
            {
                return false;
            }
        }

        public virtual bool Xor
        {
            get
            {
                return false;
            }
        }
    }
}
