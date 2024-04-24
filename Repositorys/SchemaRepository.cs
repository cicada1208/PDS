using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Repositorys
{
    public class SchemaRepository : BaseRepository<Schema>
    {
        public ApiResult<string> CreateModel(Schema schema, int option = 0)
        {
            DBUtil dbUtil = null;
            string dbTypeVar = string.Empty;
            string dbNameVar = string.Empty;
            DataTable dtSchema = null;
            DataTable dtTables = null;
            var modelBuilder = new StringBuilder();

            if (schema.DBName == DBParam.DBName.NIS)
            {
                dbUtil = DB.NIS;
                dbTypeVar = nameof(DBParam.DBType.SYBASE);
                dbNameVar = nameof(DBParam.DBName.NIS);
            }
            else if (schema.DBName == DBParam.DBName.SYB1)
            {
                dbUtil = DB.Syb1;
                dbTypeVar = nameof(DBParam.DBType.SYBASE);
                dbNameVar = nameof(DBParam.DBName.SYB1);
            }
            else if (schema.DBName == DBParam.DBName.SYB2)
            {
                dbUtil = DB.Syb2;
                dbTypeVar = nameof(DBParam.DBType.SYBASE);
                dbNameVar = nameof(DBParam.DBName.SYB2);
            }
            else if (schema.DBName == DBParam.DBName.PeriExam)
            {
                dbUtil = DB.PeriExam;
                dbTypeVar = nameof(DBParam.DBType.SQLSERVER);
                dbNameVar = nameof(DBParam.DBName.PeriExam);
            }
            else if (schema.DBName == DBParam.DBName.PeriPhery)
            {
                dbUtil = DB.PeriPhery;
                dbTypeVar = nameof(DBParam.DBType.SQLSERVER);
                dbNameVar = nameof(DBParam.DBName.PeriPhery);
            }
            else if (schema.DBName == DBParam.DBName.Inf)
            {
                dbUtil = DB.Inf;
                dbTypeVar = nameof(DBParam.DBType.SQLSERVER);
                dbNameVar = nameof(DBParam.DBName.Inf);
            }
            else if (schema.DBName == DBParam.DBName.UAAC)
            {
                dbUtil = DB.UAAC;
                dbTypeVar = nameof(DBParam.DBType.SQLSERVER);
                dbNameVar = nameof(DBParam.DBName.UAAC);
            }
            else if (schema.DBName == DBParam.DBName.MISSYS)
            {
                dbUtil = DB.MISSYS;
                dbTypeVar = nameof(DBParam.DBType.SQLSERVER);
                dbNameVar = nameof(DBParam.DBName.MISSYS);
            }

            dtSchema = dbUtil.Query(schema.Sql);

            if (dtSchema.Rows.Count != 0)
            {
                string tableName = string.Empty;
                string className = string.Empty;
                List<string> pkList = null;
                dtTables = dtSchema.DefaultView.ToTable(true, new string[] { "BaseTableName" });
                if (dtTables.Rows.Count == 1)
                {
                    tableName = dtTables.Rows[0]["BaseTableName"].ToString();
                    // primary key 目前從這裡取得較為正確
                    ////var index_keys = dbUtil.Query<dynamic>($"sp_helpindex {tableName}").ToList().FirstOrDefault()?.index_keys;
                    //var index_keys = dbUtil.Query<Schema>($"sp_helpindex {tableName}").ToList()
                    //    .OrderBy(key => key.index_name).FirstOrDefault()?.index_keys;
                    var index_keys = dbUtil.Query<Schema>($"sp_helpindex {tableName}").ToList();
                    if (index_keys.Exists(key => key.index_name.Contains("PK")))
                        index_keys = index_keys.Where(key => key.index_name.Contains("PK")).ToList();
                    if (index_keys.Count != 1)
                    {
                        index_keys = index_keys.Where(key =>
                        {
                            bool rst = false;
                            foreach (var desp in key.index_description.Split(','))
                                if (desp.Trim() == "unique")
                                {
                                    rst = true;
                                    break;
                                }
                            return rst;
                        }).ToList();
                        if (index_keys.Count != 1)
                            index_keys = index_keys.Where(key =>
                            {
                                bool rst = false;
                                foreach (var desp in key.index_description.Split(','))
                                    if (desp.Trim() == "clustered")
                                    {
                                        rst = true;
                                        break;
                                    }
                                return rst;
                            }).ToList();
                    }
                    var pk = index_keys.OrderBy(key => key.index_name).FirstOrDefault()?.index_keys;
                    pkList = (pk as string)?.Split(',').Select(key => key = key.Trim()).ToList();
                }
                foreach (DataRow row in dtTables.Rows)
                    className += (className != string.Empty ? "_" : string.Empty) +
                        Regex.Replace(row["BaseTableName"].ToString(), "^ni_", string.Empty).ToUpperFirstChar();

                switch (option)
                {
                    case 0: // C#
                    case 1: // WPF
                        SetCSAndWPF(option, modelBuilder, dbTypeVar, dbNameVar,
                            tableName, className, dtSchema, pkList);
                        break;
                    case 2: // TS
                        SetTS(option, modelBuilder, className, dtSchema);
                        break;
                }
            }

            //do
            //{
            //    if (reader.FieldCount <= 1) continue;

            //    var schema = reader.GetSchemaTable();
            //    foreach (DataRow row in schema.Rows)
            //    {
            //        if (string.IsNullOrWhiteSpace(builder.ToString()))
            //        {
            //            var tableName = row["BaseTableName"];
            //            builder.AppendFormat("public class {0}{1}", tableName, Environment.NewLine);
            //            builder.AppendLine("{");
            //        }

            //        var type = (Type)row["DataType"];
            //        var name = TypeAliases.ContainsKey(type) ? TypeAliases[type] : type.Name;
            //        var isNullable = (bool)row["AllowDBNull"] && NullableTypes.Contains(type);
            //        var collumnName = (string)row["ColumnName"];

            //        builder.AppendLine(string.Format("\tpublic {0}{1} {2} {{ get; set; }}", name, isNullable ? "?" : string.Empty, collumnName));
            //        builder.AppendLine();
            //    }

            //    builder.AppendLine("}");
            //    builder.AppendLine();
            //} while (reader.NextResult());

            dtSchema?.Dispose();
            dtTables?.Dispose();

            return new ApiResult<string>(modelBuilder.ToString());
        }

        private void SetCSAndWPF(int option, StringBuilder modelBuilder,
            string dbTypeVar, string dbNameVar, string tableName, string className,
            DataTable dtSchema, List<string> pkList)
        {
            string tab = new String(' ', 4);

            modelBuilder.AppendLine("using Params;");
            modelBuilder.AppendLine("using System;");
            modelBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
            modelBuilder.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            modelBuilder.AppendLine();
            modelBuilder.AppendLine("namespace Models");
            modelBuilder.AppendLine("{");

            modelBuilder.AppendLine($"{tab}[Lib.Attributes.Table(DBParam.DBType.{dbTypeVar}, DBParam.DBName.{dbNameVar}" +
            $"{(tableName != string.Empty ? $", \"{tableName}\"" : string.Empty)})]");
            switch (option)
            {
                case 0: // C#
                    modelBuilder.AppendLine($"{tab}public class {className}");
                    break;
                case 1:  // WPF
                    modelBuilder.AppendLine($"{tab}public class {className} : BaseModel<{className}>");
                    break;
            }
            modelBuilder.AppendLine($"{tab}{{");

            foreach (DataRow row in dtSchema.Rows)
            {
                var type = (Type)row["DataType"];
                var typeName = DBParam.ColumnTypeAliases.ContainsKey(type) ? DBParam.ColumnTypeAliases[type] : type.Name;
                // NullableTypes: 這些型別強制Nullable，避免new Model時會有預設值的狀況
                //var isNullable = (bool)row["AllowDBNull"] && NullableTypes.Contains(type);
                var isNullable = DBParam.ColumnNullableTypes.Contains(type);
                var collumnName = (string)row["ColumnName"];
                var isKey = pkList?.Contains(collumnName) ?? false; //(bool)row["IsKey"];

                switch (option)
                {
                    case 0: // C#
                        if (isKey) modelBuilder.AppendLine($"{tab + tab}[Key]");
                        modelBuilder.AppendLine($"{tab + tab}public {typeName}{(isNullable ? "?" : string.Empty)} {collumnName} {{ get; set; }}");
                        break;
                    case 1: // WPF
                        modelBuilder.AppendLine($"{tab + tab}private {typeName}{(isNullable ? "?" : string.Empty)} _{collumnName};");
                        if (isKey) modelBuilder.AppendLine($"{tab + tab}[Key]");
                        modelBuilder.AppendLine($"{tab + tab}public {typeName}{(isNullable ? "?" : string.Empty)} {collumnName}");
                        modelBuilder.AppendLine($"{tab + tab}{{");
                        modelBuilder.AppendLine($"{tab + tab + tab}get => _{collumnName};");
                        modelBuilder.AppendLine($"{tab + tab + tab}set => Set(ref _{collumnName}, value);");
                        modelBuilder.AppendLine($"{tab + tab}}}");
                        break;
                }
                modelBuilder.AppendLine();
            }

            modelBuilder.AppendLine($"{tab}}}");
            modelBuilder.AppendLine("}");
        }

        private void SetTS(int option, StringBuilder modelBuilder, string className, DataTable dtSchema)
        {
            string tab = new String(' ', 2);
            var interfaceBuilder = new StringBuilder();
            var paramBuilder = new StringBuilder();
            var thisBuilder = new StringBuilder();

            foreach (DataRow row in dtSchema.Rows)
            {
                var type = (Type)row["DataType"];
                var typeName = DBParam.ColumnTypeTSAliases.ContainsKey(type) ? DBParam.ColumnTypeTSAliases[type] : type.Name;
                var collumnName = (string)row["ColumnName"];
                string defaultVal = type == typeof(string) ? "''" : "null";
                interfaceBuilder.AppendLine($"{tab}{collumnName}?: {typeName};");
                paramBuilder.AppendLine($"{tab + tab}{collumnName} = {defaultVal},");
                thisBuilder.AppendLine($"{tab + tab}this.{collumnName} = {collumnName};");
            }

            modelBuilder.AppendLine($"export interface I{className} {{");
            modelBuilder.Append($"{interfaceBuilder}");
            modelBuilder.AppendLine($"}}");
            modelBuilder.AppendLine();
            modelBuilder.AppendLine($"export default class {className} implements I{className} {{");
            modelBuilder.Append($"{interfaceBuilder.ToString().Replace("?", "")}");
            modelBuilder.AppendLine();
            modelBuilder.AppendLine($"{tab}constructor({{");
            modelBuilder.AppendLine($"{paramBuilder.ToString().TrimEnd('\r', '\n', ',')}");
            modelBuilder.AppendLine($"{tab}}}: I{className} = {{}}) {{");
            modelBuilder.Append($"{thisBuilder}");
            modelBuilder.AppendLine($"{tab}}}");
            modelBuilder.AppendLine("}");
        }

        public string ModelCsToTs(string csModel)
        {
            string tab = new String(' ', 2);
            var modelBuilder = new StringBuilder();
            var interfaceBuilder = new StringBuilder();
            var paramBuilder = new StringBuilder();
            var thisBuilder = new StringBuilder();

            // public class Mi_mbed_Ext : Mi_mbed
            // public bool? isActive
            // public List<Ch_dnr> DNR
            // public virtual string bed_pat_no
            MatchCollection lines = Regex.Matches(csModel, @"\s*public{1}[\w\s\?<>:]+"); // @"\s*public{1}\s+\w+\<*\w*\>*\?*\s+\w+"
            var props = lines.Cast<Match>().ToList().Select(line =>
            {
                MatchCollection words = Regex.Matches(line.Value, @"\w+<*\w*>*\?*");
                if (words.Cast<object>().ToList().Exists(word => word.ToString() == "class"))
                {
                    string dName = "";
                    if (words.Count >= 4) dName = words?[3].Value;
                    return new { type = words?[1].Value, name = words?[2].Value, derivedName = dName };
                }
                else
                    return new { type = words?[words.Count - 2].Value.Replace("?", ""), name = words?[words.Count - 1].Value, derivedName = "" };
            });

            string className = props.FirstOrDefault(p => p.type == "class")?.name;
            string derivedName = props.FirstOrDefault(p => p.type == "class")?.derivedName;
            props = props.Where(p => p.type != "class");

            Dictionary<string, string> ColumnTypeTSAliases = DBParam.ColumnTypeTSAliases.ToDictionary(
                t => DBParam.ColumnTypeAliases.ContainsKey(t.Key) ? DBParam.ColumnTypeAliases[t.Key] : t.Key.Name,
                t => t.Value);

            string typeName = string.Empty;
            string collumnName = string.Empty;
            string defaultVal = string.Empty;
            foreach (var prop in props)
            {
                if (Regex.IsMatch(prop.type, @"<\w+>"))
                {
                    typeName = $"Array{Regex.Match(prop.type, @"<\w+>").Value}";
                    defaultVal = "[]";
                }
                else
                {
                    typeName = ColumnTypeTSAliases.ContainsKey(prop.type) ? ColumnTypeTSAliases[prop.type] : prop.type;
                    defaultVal = prop.type == "string" ? "''" : "null";
                }
                collumnName = prop.name;
                interfaceBuilder.AppendLine($"{tab}{collumnName}?: {typeName};");
                paramBuilder.AppendLine($"{tab + tab}{collumnName} = {defaultVal},");
                thisBuilder.AppendLine($"{tab + tab}this.{collumnName} = {collumnName};");
            }

            modelBuilder.AppendLine($"export interface I{className}{(derivedName == "" ? "" : $" extends I{derivedName}")} {{");
            modelBuilder.Append($"{interfaceBuilder}");
            modelBuilder.AppendLine($"}}");
            modelBuilder.AppendLine();
            modelBuilder.AppendLine($"export default class {className}{(derivedName == "" ? "" : $" extends {derivedName}")} implements I{className} {{");
            modelBuilder.Append($"{interfaceBuilder.ToString().Replace("?", "")}");
            modelBuilder.AppendLine();
            modelBuilder.AppendLine($"{tab}constructor({{");
            if (!derivedName.IsNullOrWhiteSpace())
                paramBuilder.AppendLine($"{tab + tab}...restProps");
            modelBuilder.AppendLine($"{paramBuilder.ToString().TrimEnd('\r', '\n', ',')}");
            modelBuilder.AppendLine($"{tab}}}: I{className} = {{}}) {{");
            if (!derivedName.IsNullOrWhiteSpace())
                modelBuilder.AppendLine($"{tab + tab}super(restProps);");
            modelBuilder.Append($"{thisBuilder}");
            modelBuilder.AppendLine($"{tab}}}");
            modelBuilder.AppendLine("}");

            return modelBuilder.ToString();
        }

    }
}
