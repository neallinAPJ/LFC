using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace LFC.DataProvider.SQLHelper
{
    public class SQLHelper : ISQLHelper
    {
        public string GetSQLUpdateStatement<T>(T t, Attribute lookupAttribute)
        {
            StringBuilder sb = new StringBuilder();
            List<string> setValues = new List<string>();
            List<string> whereValues = new List<string>();
            string tableName = t.GetType().ToString();
            sb.AppendLine($"Update {t.GetType().Name} SET ");
            int count = t.GetType().GetProperties().Count();
            for (int i = 0; i < count; i++)
            {
                string fieldName = t.GetType().GetProperties()[i].Name;
                PropertyDescriptor prop = TypeDescriptor.GetProperties(t)[fieldName];
                if (prop.Attributes.Contains(lookupAttribute))
                {
                    setValues.Add($" {fieldName} = @{fieldName} ");
                }
            }
            if (setValues.Count == 0) { throw new Exception("Nothing to update in SQL"); }
            sb.AppendLine(string.Join(",", setValues));

            sb.AppendLine("Where ");
            Dapper.Contrib.Extensions.ExplicitKeyAttribute attKey = new Dapper.Contrib.Extensions.ExplicitKeyAttribute();
            Dapper.Contrib.Extensions.KeyAttribute attKeyNormal = new Dapper.Contrib.Extensions.KeyAttribute();
            for (int i = 0; i < count; i++)
            {
                string fieldName = t.GetType().GetProperties()[i].Name;
                PropertyDescriptor prop = TypeDescriptor.GetProperties(t)[fieldName];
                if (prop.Attributes.Contains(attKey))
                {
                    whereValues.Add($"{fieldName} = @{fieldName} ");
                }
                else if (prop.Attributes.Contains(attKeyNormal))
                {
                    whereValues.Add($"{fieldName} = @{fieldName} ");
                }
            }
            sb.AppendLine(string.Join(" AND ", whereValues));
            if (whereValues.Count == 0) { throw new Exception("No key is define in Model"); }
            return sb.ToString();
        }

        public string GetSQLInsertTempTableStatement<T>()
        {
            var selectValue = $"INSERT INTO #{typeof(T).Name} {GetInsertFieldStatement<T>(false)} VALUES {GetInsertFieldStatement<T>(true)}";
            return selectValue;
        }

        public string GetSQLCreateTempTableStatement<T>()
        {
            var selectValue = $"SELECT top 0 * INTO #{typeof(T).Name} FROM {typeof(T).Name}";
            return selectValue;
        }

        public bool HasIdentityFieldInTable<T>()
        {
            Dapper.Contrib.Extensions.KeyAttribute attKeyNormal = new Dapper.Contrib.Extensions.KeyAttribute();

            int count = typeof(T).GetProperties().Count();
            for (int i = 0; i < count; i++)
            {
                string fieldName = typeof(T).GetProperties()[i].Name;
                PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T))[fieldName];
                if (prop.Attributes.Contains(attKeyNormal))
                {
                    return true;
                }
            }

            return false;
        }

        public string GetSQLCopyTempTableBackStatement<T>()
        {
            var hasIdentityField = HasIdentityFieldInTable<T>();
            string selectValue = "";
            //var selectValue = $"DELETE {typeof(T).Name}; INSERT INTO {typeof(T).Name} SELECT * FROM #{typeof(T).Name}";
            if (hasIdentityField)
            {
                selectValue += $"SET IDENTITY_INSERT {typeof(T).Name} ON ;";
            }
            selectValue += $"INSERT INTO {typeof(T).Name}{GetInsertFieldStatement<T>(false)} SELECT {GetInsertFieldStatement<T>(false, false)} FROM #{typeof(T).Name};";
            //    
            if (hasIdentityField)
            {
                selectValue += $"SET IDENTITY_INSERT {typeof(T).Name} OFF ;";
            }
            return selectValue;
        }

        public string GetSQLTruncateTempTableStatement<T>()
        {
            var selectValue = $"Truncate table #{typeof(T).Name}";
            return selectValue;
        }

        public string GetSQLSelectStatement<T>(T t)
        {
            var selectValue = $"SELECT * FROM {t.GetType().Name}";
            return selectValue;
        }

        private string GetInsertFieldStatement<T>(bool ForValuePart, bool AddParentheses = true)
        {

            Dapper.Contrib.Extensions.WriteAttribute attKey = new Dapper.Contrib.Extensions.WriteAttribute(false);
            //Dapper.Contrib.Extensions.KeyAttribute attKeyNormal = new Dapper.Contrib.Extensions.KeyAttribute();

            StringBuilder sb = new StringBuilder();
            List<string> setValues = new List<string>();
            // List<string> keyValues = new List<string>();
            string tableName = typeof(T).Name;// t.GetType().ToString();
            if (AddParentheses)
            {
                sb.Append("(");
            }
            int count = typeof(T).GetProperties().Count();
            for (int i = 0; i < count; i++)
            {
                string fieldName = typeof(T).GetProperties()[i].Name;
                PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T))[fieldName];
                if (prop.Attributes.Contains(attKey))
                {
                    foreach (var attitem in prop.Attributes)
                    {
                        if (attitem.GetType() == attKey.GetType())
                        {
                            Dapper.Contrib.Extensions.WriteAttribute w1 = (Dapper.Contrib.Extensions.WriteAttribute)attitem;
                            if (w1.Write)
                            {
                                if (ForValuePart)
                                {
                                    setValues.Add($"NULL");
                                }
                                else
                                {
                                    setValues.Add($"{fieldName}");
                                }
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (ForValuePart)
                    {
                        setValues.Add($"@{fieldName}");
                    }
                    else
                    {
                        setValues.Add($"{fieldName}");
                    }
                }

            }
            if (setValues.Count == 0) { throw new Exception("Nothing to insert in SQL. No Encrypt field nor Key Existed"); }
            sb.Append(string.Join(",", setValues));
            if (AddParentheses)
            {
                sb.Append(")");
            }
            return sb.ToString();
        }
    }
}
