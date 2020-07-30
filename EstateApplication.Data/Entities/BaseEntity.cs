using System;
using System.Collections.Generic;
using System.Text;

namespace EstateApplication.Data.Entities
{
    public abstract class BaseEntity
    {
        public string ID { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public DateTime DeleteAt { get; set; }

        public bool IsDeleted { get; set; }

    }
}
