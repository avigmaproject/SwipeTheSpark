namespace SwipeTheSpark.Models.Project
{
    public class Order_Master_DTO
    {
        public Int64 ORDM_PKeyID { get; set; }
        public Int64 ORDM_UP_PkeyID { get; set; }
        public Int64 ORDM_User_PkeyID { get; set; }
        public String? ORDM_OrderID { get; set; }
        public int ORDM_Discount_Pers { get; set; }
        public Decimal ORDM_Discount_Total { get; set; }
        public Decimal ORDM_Tot_Amount { get; set; }
        public int ORDM_IsStatus { get; set; }
        public Boolean? ORDM_IsActive { get; set; }
        public Boolean? ORDM_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64 UserID { get; set; }
    }

    public class Order_Master_DTO_Input
    {
        public int Type { get; set; }
        public Int64 ORDM_PkeyID { get; set; }
        public String? WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public String? Orderby { get; set; }
        public Int64 UserID { get; set; }
    }

}
