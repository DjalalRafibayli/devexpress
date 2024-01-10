namespace Api.Models.Dx
{
	public class Group
	{
		public string Selector { get; set; }
		public bool Desc { get; set; }
		public string GroupInterval { get; set; }
		public bool IsExpanded { get; set; }
	}

	public class GroupSummary
	{
		public string Selector { get; set; }
		public string SummaryType { get; set; }
	}

	public class GridDxModel
	{
		public bool? RequireTotalCount { get; set; }
		public bool? RequireGroupCount { get; set; }
		public bool? IsCountQuery { get; set; }
		public int? Skip { get; set; }
		public int? Take { get; set; }
		public List<Sort>? Sort { get; set; }
		public List<Group>? Group { get; set; }
		public string? Filter { get; set; }
		public List<TotalSummary>? TotalSummary { get; set; }
		public List<GroupSummary>? GroupSummary { get; set; }
		public List<string>? Select { get; set; }
		public List<string>? PreSelect { get; set; }
		public bool? RemoteSelect { get; set; }
		public bool? RemoteGrouping { get; set; }
		public List<string>? PrimaryKey { get; set; }
		public string? DefaultSort { get; set; }
		public bool? StringToLower { get; set; }
	}


	public class Sort
	{
		public string Selector { get; set; }
		public bool Desc { get; set; }
	}

	public class TotalSummary
	{
		public string Selector { get; set; }
		public string SummaryType { get; set; }
	}
}
