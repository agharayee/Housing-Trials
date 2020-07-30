using System;
using System.Collections.Generic;
using System.Text;

namespace EstateApplication.Data.Entities
{
    public class Contact : BaseEntity
    {
        public string State { get; set; }

        public string LocalGovernmentArea { get; set; }
    }
}
