namespace SwipeTheSpark.Models.Project
{
    public class Debate_Master_DTO
    {
        public Int64 DM_PKeyID { get; set; }
        public Int64 DM_DTM_PkeyID { get; set; }
        public Int64 DM_DUM_Main_PKeyID { get; set; }
        public Int64 DM_DUM_Opposite_PKeyID { get; set; }
        public int DM_IsAccepted { get; set; }
        public Boolean? DM_IsActive { get; set; }
        public Boolean? DM_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64 UserID { get; set; }
    }

    public class Debate_Master_DTO_Input
    {
        public int Type { get; set; }
        public Int64 DM_PkeyID { get; set; }
        public Int64 DM_DUM_Main_PKeyID { get; set; }
        public String? WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public String? Orderby { get; set; }
        public Int64 UserID { get; set; }
    }
}
