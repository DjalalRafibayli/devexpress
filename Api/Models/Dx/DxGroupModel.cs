namespace Api.Models.Dx
{
    public class DxGroupModel
    {
        public DxGroupModel()
        {
            this.items = new List<DxGroupModel> ();
        }
        public string key { get; set; }
        public List<DxGroupModel> items { get; set; }
        public int count { get; set; }
    }
}
