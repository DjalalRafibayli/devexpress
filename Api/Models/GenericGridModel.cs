namespace Api.Models
{
    public class GenericGridModel<T> 
    {
        public T Data { get; set; }
        public int totalCount { get; set; }
    }
}
