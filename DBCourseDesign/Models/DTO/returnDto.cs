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
}
