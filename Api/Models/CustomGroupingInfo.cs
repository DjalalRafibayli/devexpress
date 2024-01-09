namespace Api.Models
{
    public class CustomGroupingInfo : CustomSortingInfo
    {
        //
        // Summary:
        //     A value that groups data in ranges of a given length or date/time period.
        public string GroupInterval { get; set; }

        //
        // Summary:
        //     A flag indicating whether the group's data objects should be returned.
        public bool? IsExpanded { get; set; }

        //
        // Summary:
        //     Returns the value of the IsExpanded field or true if this value is null.
        //
        // Returns:
        //     The value of the IsExpanded field or true if this value is null.
        public bool GetIsExpanded()
        {
            if (!IsExpanded.HasValue)
            {
                return true;
            }

            return IsExpanded.Value;
        }
    }
}
