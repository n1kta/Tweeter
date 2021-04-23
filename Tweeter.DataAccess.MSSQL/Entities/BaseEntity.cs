using System;

namespace Tweeter.DataAccess.MSSQL.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
