namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;

    //wyc
    public partial class returnDto<T>
    {
        public returnDto(T input, string info)
        {
            data = input;
            this.info = info;
        }
        public T data { get; set; }
        public string info { get; set; }
    }
    public partial class returnHelper
    {
        public static returnDto<T> make<T>(T data, string info = "ok")
        {
            return new returnDto<T>(data, info);
        }
        public static returnDto<int> fail(string info = "fail")
        {
            return new returnDto<int>(0,info);
        }
    }
}
