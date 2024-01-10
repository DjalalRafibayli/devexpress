using Api.Models.Dx;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Api.Method
{
	public static class SqlGenerator
	{
		public static string Generate(GridDxModel model, string TableName)
		{
			return $"SELECT * FROM {TableName} WHERE 1=1 " +
				model.Filter ?? " and " + GenerateSqlQueryFilter(model.Filter) +
				model.Sort ?? GenerateSort(model.Sort) +
				FetchRow((int)model.Skip, (int)model.Take);
		}
		static string GenerateSqlQueryFilter(string json)
		{
			StringBuilder sb = new StringBuilder();

			JToken token = JToken.Parse(json);

			// Convert the parsed token to a C# array
			object[] resultArray = token.ToObject<object[]>();

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
								string field = '[' + conditionArray[0].ToString().ToLower() + ']';
								string comparison = conditionArray[1].ToString();
								string value = conditionArray[2].ToString();

								sb.Append($"{field} {ComparisonSqlGenerator(comparison, TryParseValueChangeType(value))}");
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
				//Console.WriteLine(item);
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
			if (DateTime.TryParseExact(inputString, "M/d/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
			{
				// Format the DateTime as a string in the desired format
				string formattedDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

				return $"'{formattedDate}'";
			}
			else
			{
				return inputString;
			}

		}
		//static bool HaveSubJToken(string item)
		//{
		//	try
		//	{
		//		bool result = false;
		//		JToken token = JToken.Parse(item.ToString());
		//		object[] resultArray = token.ToObject<object[]>();
		//		for (int i = 0; i < token.Count(); i++)
		//		{
		//			if (token is JArray conditionArray)
		//			{
		//				result =  HaveSubJToken(conditionArray[i].ToString());
		//			}
		//			else
		//			{
		//				return false;
		//			}
		//		}

		//		return result;
		//	}
		//	catch (Exception)
		//	{
		//		return false;
		//	}
		//}
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

			sb.Append($" OFFSET {skip} ROWS");
			sb.Append($"{Environment.NewLine}FETCH NEXT {take} ROWS ONLY;");

			return sb.ToString();
		}
		static string GenerateSort(List<Sort> sorts)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($" Order by ");
			bool isFirstSort = true;
			foreach (var item in sorts)
			{
				sb.Append($"{item.Selector.ToString()}");
				if (item.Desc)
				{
					sb.Append($" desc");
				}
				else
				{
					sb.Append($" asc");
				}
				if (!isFirstSort)
				{
					sb.Append(", "); // Add a comma to separate multiple sort items
				}
				isFirstSort = false;
			}
			return sb.ToString();
		}
		static string ComparisonSqlGenerator(string comparison, string value)
		{
			return comparison switch
			{
				"contains" => $" like N'%{value}%' ",
				"notcontains" => $" not like N'%{value}%' ",
				_ => StandartComparisonSqlGenerator(comparison, value)
			};
		}
		static string StandartComparisonSqlGenerator(string comparison, string value)
		{
			return $" {comparison} {value}";
		}
		//public class SortRoot
		//{
		//	public List<Sort> Sort { get; set; }
		//}

		//public class Sort
		//{
		//	public string Selector { get; set; }
		//	public bool Desc { get; set; }
		//}
	}
}
