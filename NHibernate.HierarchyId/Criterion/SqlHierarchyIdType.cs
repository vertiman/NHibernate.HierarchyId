using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Types;
using NHibernate.SqlTypes;
using NHibernate.Type;
using SqlString = System.Data.SqlTypes.SqlString;

namespace NHibernate.HierarchyId.Criterion
{
    /// <summary>
    /// Tipo para los objetos HierarchyId
    /// </summary>
    [Serializable]
    internal class SqlHierarchyIdType : ImmutableType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SqlHierarchyIdType()
            : base(SqlTypeFactory.Byte)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public override void Set(System.Data.IDbCommand cmd, object value, int index)
        {
            string parameterValue = value as string;

            SqlParameter sqlParameter = (SqlParameter)cmd.Parameters[index];
            sqlParameter.SqlDbType = SqlDbType.Udt;
            sqlParameter.UdtTypeName = "hierarchyid";
            sqlParameter.Value = SqlHierarchyId.Parse(new SqlString(parameterValue));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public override object FromStringValue(string xml)
        {
            return SqlHierarchyId.Parse(new SqlString(xml));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public override object Get(System.Data.IDataReader rs, string name)
        {
            return Get(rs, rs.GetOrdinal(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public override object Get(System.Data.IDataReader rs, int index)
        {
            return (SqlHierarchyId)rs[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public override string ToString(object val)
        {
            return val.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get { return ReturnedClass.Name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override System.Type ReturnedClass
        {
            get { return typeof(SqlHierarchyId); }
        }
    }
}