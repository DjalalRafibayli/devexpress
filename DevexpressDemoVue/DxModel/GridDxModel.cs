namespace DevexpressDemoVue.DxModel
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Group
    {
        public string selector { get; set; }
        public bool desc { get; set; }
        public string groupInterval { get; set; }
        public bool isExpanded { get; set; }
    }

    public class GroupSummary
    {
        public string selector { get; set; }
        public string summaryType { get; set; }
    }

    public class GridDxModel
    {
        public bool requireTotalCount { get; set; }
        public bool requireGroupCount { get; set; }
        public bool isCountQuery { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public List<Sort> sort { get; set; }
        public List<Group> group { get; set; }
        public List<object> filter { get; set; }
        public List<TotalSummary> totalSummary { get; set; }
        public List<GroupSummary> groupSummary { get; set; }
        public List<string> select { get; set; }
        public List<string> preSelect { get; set; }
        public bool remoteSelect { get; set; }
        public bool remoteGrouping { get; set; }
        public List<string> primaryKey { get; set; }
        public string defaultSort { get; set; }
        public bool stringToLower { get; set; }
    }

    public class Sort
    {
        public string selector { get; set; }
        public bool desc { get; set; }
    }

    public class TotalSummary
    {
        public string selector { get; set; }
        public string summaryType { get; set; }
    }
}
