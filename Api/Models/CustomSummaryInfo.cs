namespace Api.Models
{
    public class CustomSummaryInfo
    {
        //
        // Summary:
        //     The data field to be used for calculating the summary.
        public string Selector { get; set; }

        //
        // Summary:
        //     An aggregate function: "sum", "min", "max", "avg", or "count".
        public string SummaryType { get; set; }
    }
}
