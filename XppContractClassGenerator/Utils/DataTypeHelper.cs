using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XppContractClassGenerator
{
    class DataTypeHelper
    {
        public static string ToXppPrimitiveType(DataType type)
        {
            string str = null;
            switch (type)
            {
                case DataType.STR:
                    str = "str";
                    break;
                case DataType.INT:
                    str = "int";
                    break;
                case DataType.REAL:
                    str = "real";
                    break;
                case DataType.INT64:
                    str = "int64";
                    break;
                case DataType.DATE:
                    str = "date";
                    break;
                case DataType.ENUM:
                    str = "enum";
                    break;
                case DataType.UTCDATETIME:
                    str = "utcdatetime";
                    break;
                case DataType.BOOLEAN:
                    str = "boolean";
                    break;
                case DataType.OBJECT:
                    str = "Object";
                    break;
                case DataType.LIST:
                    str = "List";
                    break;
                case DataType.CONTAINER:
                    str = "container";
                    break;
                case DataType.SET:
                    str = "Set";
                    break;
            }
            return str;
        }

        public static string ToXppTypesEnum(DataType type)
        {
            string str = "";
            switch (type)
            {
                case DataType.STR:
                    str = "Types::String";
                    break;
                case DataType.INT:
                    str = "Types::Integer";
                    break;
                case DataType.REAL:
                    str = "Types::Real";
                    break;
                case DataType.INT64:
                    str = "Types::Int64";
                    break;
                case DataType.DATE:
                    str = "Types::Date";
                    break;
                case DataType.UTCDATETIME:
                    str = "Types::UtcDateTime";
                    break;
                case DataType.BOOLEAN:
                    str = "Types::Boolean";
                    break;
                case DataType.OBJECT:
                    str = "Types::Class";
                    break;
                case DataType.LIST:
                    str = "Types::Class";
                    break;
                case DataType.CONTAINER:
                    str = "Types::Container";
                    break;
                case DataType.SET:
                    str = "Types::Class";
                    break;
            }
            return str;
        }

        public static DataType FromJTokenType(JTokenType tokenType)
        {
            DataType dataType = DataType.NONE;
            switch (tokenType)
            {
                case JTokenType.Boolean:
                    dataType = DataType.BOOLEAN;
                    break;
                case JTokenType.Date:
                    dataType = DataType.DATE;
                    break;
                case JTokenType.Integer:
                    dataType = DataType.INT;
                    break;
                case JTokenType.Float:
                    dataType = DataType.REAL;
                    break;
                case JTokenType.String:
                    dataType = DataType.STR;
                    break;
                case JTokenType.Object:
                    dataType = DataType.OBJECT;
                    break;
                case JTokenType.Array:
                    dataType = Static.GetApplicationOptions().CollectionDataType;
                    break;
            }
            return dataType;
        }

        public static bool IsInstantiable(DataType type)
        {
            bool ret = false;
            switch (type)
            {
                case DataType.LIST:
                    ret = true;
                    break;
                case DataType.OBJECT:
                    ret = true;
                    break;
                case DataType.SET:
                    ret = true;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }

        public static bool IsCollection(DataType type)
        {
            bool ret = false;
            switch (type)
            {
                case DataType.LIST:
                    ret = true;
                    break;
                case DataType.CONTAINER:
                    ret = true;
                    break;
                case DataType.SET:
                    ret = true;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }

        public static bool IsTypedCollection(DataType type)
        {
            bool ret = false;
            switch (type)
            {
                case DataType.LIST:
                    ret = true;
                    break;
                case DataType.SET:
                    ret = true;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }
    }
}
