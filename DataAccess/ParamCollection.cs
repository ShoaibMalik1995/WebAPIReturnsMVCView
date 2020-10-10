using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class ParamCollection : List<System.Data.IDataParameter>
    {
        public void Add(IDataParameter p_Param) { base.Add(p_Param); }
        public void Add(IDataParameter p_Param, DbType p_Type)
        {
            p_Param.DbType = p_Type;
            base.Add(p_Param);
        }
        public IDataParameter Item(int p_Index) { return base[p_Index]; }
        public void Remove(int p_Index) { base.RemoveAt(p_Index); }
        public int Count { get { return base.Count; } }

    }
}
