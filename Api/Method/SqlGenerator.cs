using Api.Models.Dx;
using DevExtreme.AspNet.Mvc.Builders;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Xml.Linq;

namespace Api.Method
{
    public static class SqlGenerator
    {
        public static (string sqlQuery, string totalCountQuery) Generate(GridDxModel model, string TableName)
        {
            if (model.Group?.Count() > 0)
            {
                return (GroupByExpression(model.Group, TableName), string.Empty);
            }


            string sqlQuery = $"SELECT * FROM {TableName} WHERE 1=1";
            string totalCountQuery = $"SELECT Count(*) FROM {TableName} WHERE 1=1";
            if (!string.IsNullOrEmpty(model.Filter))
            {
                sqlQuery += $" and {GenerateSqlQueryFilter(model.Filter)}";
                totalCountQuery += $" and {GenerateSqlQueryFilter(model.Filter)}";
            }

            if (model.Sort?.Count() > 0)
            {
                sqlQuery += $" {GenerateSort(model.Sort)}";
            }

            sqlQuery += FetchRow((int)model.Skip, (int)model.Take);

            return (sqlQuery, totalCountQuery);
        }
        static string GroupByExpression(List<Group> groups, string TableName)
        {
            //SELECT COUNT(*) AS[I0], [m].[Name] AS[I1]
            //FROM[Movies] AS[m]
            //GROUP BY[m].[Name]
            //ORDER BY[m].[Name]

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(*) AS [I0],");
            int i = 1;

            foreach (Group group in groups)
            {
                sb.Append($" [m].[{group.Selector}] AS[I{i}] ");
                i++;
            }

            sb.Append($" FROM[{TableName}] AS[m] ");

            foreach (Group group in groups)
            {
                i = 0;
                sb.Append($" GROUP BY[m].[{group.Selector}] ");
                if (i < groups.Count - 1)
                {
                    sb.Append(',');
                }
                i++;
            }
            //sort order by
            foreach (Group group in groups)
            {
                i = 0;
                sb.Append($" ORDER BY[m].[{group.Selector}] ");
                if (group.Desc)
                {
                    sb.Append(" desc ");
                }
                if (i < groups.Count - 1)
                {
                    sb.Append(',');
                }
                i++;
            }
            return sb.ToString();
        }
        static string GenerateSqlQueryFilter(string json)
        {
            StringBuilder sb = new StringBuilder();

            JToken token = JToken.Parse(json);

            // Convert the parsed token to a C# array
            object[] resultArray = token.ToObject<object[]>();

            /// filter (columnName = 152)
            if (!HaveSubJToken(token.ToString()))
            {
                if (token is JArray conditionArray)
                {
                    sb.Append(StaticSqlFilter(conditionArray));
                }
            }
            else
            {
                foreach (var item in resultArray)
                {
                    if (item.ToString().ToLower() == "and" || item.ToString().ToLower() == "or")
                    {
                        sb.Append($" {item.ToString().ToLower()} ");
                    }
                    else
                    {
                        var sub = TryParseJToken(item.ToString());
                        if (sub.token is JArray conditionArray)
                        {
                            if (sub.token.Count() == 3)
                            {
                                bool haveSubToken = HaveSubJToken(item.ToString());
                                if (!haveSubToken)
                                {
                                    sb.Append(StaticSqlFilter(conditionArray));
                                }
                                else
                                {
                                    sb.Append(GenerateSqlQueryFilter(item.ToString()));
                                }
                            }
                            else
                            {
                                GenerateSqlQueryFilter(item.ToString());
                            }
                        }
                        else
                        {
                            sb.Append($" {item.ToString().ToLower()} ");
                        }
                    }
                }
            }

            return sb.ToString();
        }
        static (JToken token, object[] array) TryParseJToken(string item)
        {
            try
            {
                JToken token = JToken.Parse(item.ToString());
                object[] resultArray = token.ToObject<object[]>();
                return (token, resultArray);
            }
            catch (Exception)
            {
                return (null, null);
            }
        }
        static string TryParseValueChangeType(string inputString)
        {
            try
            {
                var datetime = Convert.ToDateTime(inputString);
                string formattedDate = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                return $"'{formattedDate}'";
            }
            catch (Exception)
            {
                return inputString;
            }

        }
        static bool HaveSubJToken(string item)
        {
            JToken token = JToken.Parse(item.ToString());
            if (token.Type == JTokenType.Array)
            {
                foreach (var childToken in token.Children())
                {
                    if (childToken.Type == JTokenType.Array)
                    {
                        // Recursive call to check subarrays
                        //if (HaveSubJToken(childToken.ToString()))
                        //{
                        //	return true;
                        //}
                        return true;
                    }
                }
            }

            return false;
        }

        static string FetchRow(int skip, int take)
        {
            StringBuilder sb = new StringBuilder();
            if (take > 0)
            {
                sb.Append($" OFFSET {skip} ROWS");
                sb.Append($"{Environment.NewLine}FETCH NEXT {take} ROWS ONLY;");
            }

            return sb.ToString();
        }
        static string GenerateSort(List<Sort> sorts)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($" Order by ");
            int i = 1;
            foreach (var item in sorts)
            {
                sb.Append($"{item.Selector.ToString()}");
                if (item.Desc)
                {
                    sb.Append($" desc ");
                }
                else
                {
                    sb.Append($" asc ");
                }
                if ((i == 1 && sorts.Count() > 1) || (i != 1 && i != sorts.Count()))
                {
                    sb.Append(", "); // Add a comma to separate multiple sort items
                }
                i++;
            }
            return sb.ToString();
        }
        static string ComparisonSqlGenerator(string comparison, string value)
        {
            return comparison switch
            {
                "contains" => $" like N'%{value}%' ",
                "notcontains" => $" not like N'%{value}%' ",
                _ => StandartComparisonSqlGenerator(comparison, value)/// example = value
            };
        }
        static string StandartComparisonSqlGenerator(string comparison, string value)
        {
            return $" {comparison} {value}";
        }
        static string StaticSqlFilter(JArray conditionArray)
        {
            string field = '[' + conditionArray[0].ToString().ToLower() + ']';
            string comparison = conditionArray[1].ToString();
            string value = conditionArray[2].ToString();

            return $"{field} {ComparisonSqlGenerator(comparison, TryParseValueChangeType(value))}";
        }
    }
}
