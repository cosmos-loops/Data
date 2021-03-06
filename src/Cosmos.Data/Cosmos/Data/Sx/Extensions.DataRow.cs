using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Reflection;
using Cosmos.Conversions;

namespace Cosmos.Data.Sx
{
    /// <summary>
    /// Extensions for <see cref="DataRow"/>
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// To entity
        /// </summary>
        /// <param name="row"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToEntity<T>(this DataRow row) where T : new()
        {
            row.CheckNull(nameof(row));
          
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var entity = new T();

            foreach (var property in properties)
            {
                if (row.Table.Columns.Contains(property.Name))
                {
                    var valueType = property.PropertyType;
                    property.SetValue(entity, row[property.Name].CastTo(valueType), null);
                }
            }

            foreach (var field in fields)
            {
                if (row.Table.Columns.Contains(field.Name))
                {
                    var valueType = field.FieldType;
                    field.SetValue(entity, row[field.Name].CastTo(valueType));
                }
            }

            return entity;
        }

        /// <summary>
        /// To expando object
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static dynamic ToExpandoObject(this DataRow row)
        {
            row.CheckNull(nameof(row));
           
            dynamic entity = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>) entity;

            foreach (DataColumn column in row.Table.Columns)
            {
                expandoDict.Add(column.ColumnName, row[column]);
            }

            return expandoDict;
        }
    }
}