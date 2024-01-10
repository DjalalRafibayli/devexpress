using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EntityLayer.Models;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

using System.ComponentModel;
using Api.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using EntityLayer.Dapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Api.Models.Dx;
using Api.Method;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	public class MoviesController : Controller
	{
		private readonly MovieDbContext _context;
		private readonly DapperContext _dapperContext;
		public MoviesController(MovieDbContext context, DapperContext dapperContext)
		{
			_context = context;
			_dapperContext = dapperContext;
		}

		[HttpPost]
		[Route("GetData")]
		//public IActionResult GetData(DataSourceLoadOptionsBase loadOptions)
		//{
		//    var movies = _context.Movies.AsQueryable(); // Assuming _context.Movies is your movie data source

		//    if (loadOptions.Filter != null)
		//    {
		//        var filterExpression = GenerateFilterExpression<Movie>(loadOptions.Filter.ToString());
		//        //movies= movies.Where(loadOptions.Filter.ToString());
		//        movies = movies.Where(filterExpression);
		//    }
		//    // Apply sorting
		//    if (loadOptions.Sort != null && loadOptions.Sort.Count() > 0)
		//    {
		//        movies = ApplySort(movies, loadOptions.Sort);
		//    }

		//    // Apply paging
		//    var retrunedMovies = movies.Skip(loadOptions.Skip).Take(loadOptions.Take);

		//    var loadResult = new
		//    {
		//        data = retrunedMovies.ToList(),
		//        totalCount = movies.Count()
		//    };

		//    var data = new GenericGridModel<MovieList>() { Data = new MovieList() { movies = movies.ToList() }, totalCount = movies.Count() };

		//    return Ok(data);

		//}
		public object GetData([FromBody] GridDxModel loadOptions)
		{
			var sql = SqlGenerator.Generate(loadOptions, "Movies");
			var c = _dapperContext.CreateConnection();

			var responseQuery = c.Query<Movie>(sql);
			var responseModel = new DxGridResponseModel<Movie>
			{
				data = responseQuery.ToList(),
				totalCount = 11
			};
			return responseModel;
		}
		//public object GetData(DataSourceLoadOptionsBase loadOptions)
		//{
		//    var c = _dapperContext.CreateConnection();
		//    return DataSourceLoader.Load(c.Query<Movie>("SELECT * FROM Movies").AsQueryable(), loadOptions);
		//}
		public class MovieList
		{
			public List<Movie> movies { get; set; }
		}
		private Expression<Func<T, bool>> GenerateFilterExpression<T>(string filter)
		{
			var parameter = Expression.Parameter(typeof(T));
			var expression = DynamicExpressionParser.ParseLambda(new[] { parameter }, typeof(bool), filter);
			return (Expression<Func<T, bool>>)expression;
		}
		private IQueryable<T> ApplySort<T>(IQueryable<T> query, IList<SortingInfo> sortOptions)
		{
			var orderedQuery = query;

			foreach (var sortOption in sortOptions)
			{
				var sortExpression = $"{sortOption.Selector} {(sortOption.Desc ? "descending" : "ascending")}";
				orderedQuery = orderedQuery.OrderBy(sortExpression);
			}

			return orderedQuery;
		}
	}
}
