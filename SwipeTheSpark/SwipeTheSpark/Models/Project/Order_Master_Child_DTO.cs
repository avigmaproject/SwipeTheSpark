﻿namespace SwipeTheSpark.Models.Project
{
    public class Order_Master_Child_DTO
    {
        public Int64 ORD_PKeyID { get; set; }
        public Int64 ORD_UP_PkeyID { get; set; }
        public Int64 ORD_Pro_PkeyID { get; set; }
        public Int64 ORD_UP_User_PkeyID { get; set; }
        public Int64 ORD_UP_Purchase_PkeyID { get; set; }
        public Boolean? ORD_IsActive { get; set; }
        public Boolean? ORD_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64 UserID { get; set; }
    }

    public class Order_Master_Child_DTO_Input
    {
        public int Type { get; set; }
        public Int64 ORD_PkeyID { get; set; }
        public String? WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public String? Orderby { get; set; }
        public Int64 UserID { get; set; }
    }

}
