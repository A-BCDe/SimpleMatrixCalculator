using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Matrix_Calculator.Set
{
    class Set
    {
        private List<object> _set;
        public int Size
        {
            get
            {
                return _set.Count;
            }
        }

        public Set()
        {
            _set = new List<object>();
        }
        public Set(List<object> list)
        {
            _set = list;
        }
        public Set(int length, SpecialSet specialSet)
        {
            _set = new List<object>(length);
            switch (specialSet)
            {
                case SpecialSet.ZeroSet:
                    for (int i = 0; i < length; i++)
                    {
                        _set[i] = 0;
                    }
                    break;

                case SpecialSet.OneSet:
                    for (int i = 0; i < length; i++)
                    {
                        _set[i] = 0;
                    }
                    break;

                case SpecialSet.IncreasingSet:
                    for (int i = 0; i < length; i++)
                    {
                        _set[i] = i;
                    }
                    break;

                case SpecialSet.DecreasingSet:
                    for (int i = 0; i < length; i++)
                    {
                        _set[i] = length - i - 1;
                    }
                    break;
            }
        }

        public object this[int index]
        {
            get
            {
                return _set[index];
            }
        }
    }

    enum SpecialSet
    {
        ZeroSet, OneSet, IncreasingSet, DecreasingSet
    }
}
