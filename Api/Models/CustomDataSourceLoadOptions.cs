using DevExtreme.AspNet.Data.Helpers;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace Api.Models
{
    //[ModelBinder(BinderType = typeof(DataSourceLoadOptionsBinder))]
    public class CustomDataSourceLoadOptions : CustomDataSourceLoadOptionsBase
    {
    }

    //public class DataSourceLoadOptionsBinder : IModelBinder
    //{

    //    public Task BindModelAsync(ModelBindingContext bindingContext)
    //    {
    //        var loadOptions = new CustomDataSourceLoadOptions();
    //        CustomDataSourceLoadOptionsParser.Parse(loadOptions, key => bindingContext.ValueProvider.GetValue(key).FirstOrDefault());
    //        bindingContext.Result = ModelBindingResult.Success(loadOptions);
    //        return Task.CompletedTask;
    //    }

    //}
}
