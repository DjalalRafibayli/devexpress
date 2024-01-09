
using System.Collections;

namespace Api.Models
{
    public class CustomDataSourceLoadOptionsBase
    {
        //
        // Summary:
        //     A flag indicating whether the total number of data objects is required.
        public bool RequireTotalCount { get; set; }

        //
        // Summary:
        //     A flag indicating whether the number of top-level groups is required.
        public bool RequireGroupCount { get; set; }

        //
        // Summary:
        //     A flag indicating whether the current query is made to get the total number of
        //     data objects.
        public bool IsCountQuery { get; set; }

        //
        // Summary:
        //     The number of data objects to be skipped from the start of the resulting set.
        public int Skip { get; set; }

        //
        // Summary:
        //     The number of data objects to be loaded.
        public int Take { get; set; }

        //
        // Summary:
        //     A sort expression.
        public CustomSortingInfo[] Sort { get; set; }

        //
        // Summary:
        //     A group expression.
        public CustomGroupingInfo[] Group { get; set; }

        //
        // Summary:
        //     A filter expression.
        public IList Filter { get; set; }

        //
        // Summary:
        //     A total summary expression.
        public CustomSummaryInfo[] TotalSummary { get; set; }

        //
        // Summary:
        //     A group summary expression.
        public CustomSummaryInfo[] GroupSummary { get; set; }

        //
        // Summary:
        //     A select expression.
        public string[] Select { get; set; }

        //
        // Summary:
        //     An array of data fields that limits the DevExtreme.AspNet.Data.DataSourceLoadOptionsBase.Select
        //     expression. The applied select expression is the intersection of DevExtreme.AspNet.Data.DataSourceLoadOptionsBase.PreSelect
        //     and DevExtreme.AspNet.Data.DataSourceLoadOptionsBase.Select.
        public string[] PreSelect { get; set; }

        //
        // Summary:
        //     A flag that indicates whether the LINQ provider should execute the select expression.
        //     If set to false, the select operation is performed in memory.
        public bool? RemoteSelect { get; set; }

        //
        // Summary:
        //     A flag that indicates whether the LINQ provider should execute grouping. If set
        //     to false, the entire dataset is loaded and grouped in memory.
        public bool? RemoteGrouping { get; set; }

        //
        // Summary:
        //     An array of primary keys.
        public string[] PrimaryKey { get; set; }

        //
        // Summary:
        //     The data field to be used for sorting by default.
        public string DefaultSort { get; set; }

        //
        // Summary:
        //     A flag that indicates whether filter expressions should include a ToLower() call
        //     that makes string comparison case-insensitive. Defaults to true for LINQ to Objects,
        //     false for any other provider.
        public bool? StringToLower { get; set; }

        internal bool HasFilter
        {
            get
            {
                if (Filter != null)
                {
                    return Filter.Count > 0;
                }

                return false;
            }
        }

        internal bool HasGroups
        {
            get
            {
                if (Group != null)
                {
                    return Group.Length != 0;
                }

                return false;
            }
        }

        internal bool HasSort
        {
            get
            {
                if (Sort != null)
                {
                    return Sort.Length != 0;
                }

                return false;
            }
        }

        internal bool HasPrimaryKey
        {
            get
            {
                if (PrimaryKey != null)
                {
                    return PrimaryKey.Length != 0;
                }

                return false;
            }
        }

        internal bool HasDefaultSort => !string.IsNullOrEmpty(DefaultSort);

        internal bool HasSummary
        {
            get
            {
                if (TotalSummary == null || TotalSummary.Length == 0)
                {
                    return HasGroupSummary;
                }

                return true;
            }
        }

        internal bool HasGroupSummary
        {
            get
            {
                if (GroupSummary != null)
                {
                    return GroupSummary.Length != 0;
                }

                return false;
            }
        }

        internal bool HasAnySort
        {
            get
            {
                if (!HasGroups && !HasSort && !HasPrimaryKey)
                {
                    return HasDefaultSort;
                }

                return true;
            }
        }

        internal bool HasAnySelect
        {
            get
            {
                if (!HasPreSelect)
                {
                    return HasSelect;
                }

                return true;
            }
        }

        internal bool HasPreSelect
        {
            get
            {
                if (PreSelect != null)
                {
                    return PreSelect.Length != 0;
                }

                return false;
            }
        }

        internal bool HasSelect
        {
            get
            {
                if (Select != null)
                {
                    return Select.Length != 0;
                }

                return false;
            }
        }

        internal bool UseRemoteSelect => RemoteSelect.GetValueOrDefault(true);

        internal IEnumerable<CustomSortingInfo> GetFullSort()
        {
            HashSet<string> hashSet = new HashSet<string>();
            List<CustomSortingInfo> list = new List<CustomSortingInfo>();
            if (HasGroups)
            {
                CustomGroupingInfo[] group = Group;
                foreach (CustomGroupingInfo groupingInfo in group)
                {
                    if (!hashSet.Contains(groupingInfo.Selector))
                    {
                        hashSet.Add(groupingInfo.Selector);
                        list.Add(groupingInfo);
                    }
                }
            }

            if (HasSort)
            {
                CustomSortingInfo[] sort = Sort;
                foreach (CustomSortingInfo sortingInfo in sort)
                {
                    if (!hashSet.Contains(sortingInfo.Selector))
                    {
                        hashSet.Add(sortingInfo.Selector);
                        list.Add(sortingInfo);
                    }
                }
            }

            IEnumerable<string> enumerable = new string[0];
            if (HasDefaultSort)
            {
                enumerable = enumerable.Concat(new string[1] { DefaultSort });
            }

            if (HasPrimaryKey)
            {
                enumerable = enumerable.Concat(PrimaryKey);
            }

            return CustomUtils.AddRequiredSort(list, enumerable);
        }

        internal IEnumerable<string> GetFullSelect()
        {
            if (HasPreSelect && HasSelect)
            {
                return PreSelect.Intersect(Select);
            }

            if (HasPreSelect)
            {
                return PreSelect;
            }

            if (HasSelect)
            {
                return Select;
            }

            return Enumerable.Empty<string>();
        }
    }
}
