namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;

    //wyc
    public partial class stringReceiver
    {
        public string id { private get; set; }
        public string decoded()
        {
            return idDecorator.decode(id);
        }
    }
}
