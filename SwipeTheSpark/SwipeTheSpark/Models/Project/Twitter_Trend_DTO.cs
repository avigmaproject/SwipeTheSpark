namespace SwipeTheSpark.Models.Project
{
    public class Trend
    {
        public Int64 TDM_PKeyID { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string promoted_content { get; set; }
        public string query { get; set; }
        public int? tweet_volume { get; set; }
        public Boolean TDM_IsActive { get; set; }
        public Boolean TDM_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64 UserID { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public int woeid { get; set; }
    }

    public class RootObject
    {
        public List<Trend> trends { get; set; }
        public string as_of { get; set; }
        public string created_at { get; set; }
        public List<Location> locations { get; set; }
    }
    public class Trend_DTO_Input
    {
        public int Type { get; set; }
        public Int64 TDM_PKeyID { get; set; }
        public String? WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public String? Orderby { get; set; }
        public Int64 UserID { get; set; }
    }

}
