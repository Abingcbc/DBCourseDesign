namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;

    //wyc
    public partial class idDecorator
    {
        public static void make(string id, IdType type)
        {
            string prefix = type.ToString();
            id = prefix + id;
        }
        public enum IdType
        {
            EQ, AC, WH, ST, RS, WS, PS
        }
        public static string decode(string encodedId)
        {
            string id = encodedId.Substring(2);
            return id;
        }
    }
}
