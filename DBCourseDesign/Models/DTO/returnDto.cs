namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;

    //wyc
    public partial class returnDto<T>
    {
        public returnDto(T input)
        {
            data = input;
        }
        public T data { get; set; }
    }
    public partial class returnHelper
    {
        public static returnDto<T> make<T> (T input)
        {
            return new returnDto<T>(input);
        }
    }
}
