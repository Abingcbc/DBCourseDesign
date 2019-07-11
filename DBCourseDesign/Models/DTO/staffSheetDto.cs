namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;

    public partial class staffDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string accountID { get; set; }
        public string password { get; set; }
        public string status { get; set; }
        public List<StaffItem> detail { get; set; }
    }

    public partial class StaffItem
    {
        public StaffItem(string name,string value)
        {
            roleName = name;
            var = value;
        }
        public string roleName { get; set; }
        public string var { get; set; }
        public static List<StaffItem> makeRow(string phone,string idNumber,string C_T,string M_T,string S_T,string E_T)
        {
            var row = new List<StaffItem>();
            row.Add(new StaffItem("电话号码", phone));
            row.Add(new StaffItem("身份证号码", idNumber));
            row.Add(new StaffItem("账户创建时间", C_T));
            row.Add(new StaffItem("账户最近修改时间", M_T));
            row.Add(new StaffItem("每周工作开始时间", S_T));
            row.Add(new StaffItem("每周工作结束时间", E_T));
            return row;
        }
    }

    public partial class staffModifyReceiver
    {
        public string id { get; set; }
        public string name { get; set; }
        public string accountID { get; set; }
        public string password { get; set; }
        public string status { get; set; }
        public string telNumber { get; set; }
        public string idCardNumber { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }

    public partial class staffAddDto
    {
        public List<staffDto> data { get; set; }
        public string info1 { get; set; }
        public string info2 { get; set; }
    }

    public partial class passwordModifyDto
    {
        public string id { get; set; }
        public string newPassword { get; set; }
    }

}