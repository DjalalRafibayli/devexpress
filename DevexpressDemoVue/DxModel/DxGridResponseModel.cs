namespace DevexpressDemoVue.DxModel
{
    public class DxGridResponseModel<T> where T : class
    {
        public List<T> data { get; set; }
        public int totalCount { get; set; }
    }
}
