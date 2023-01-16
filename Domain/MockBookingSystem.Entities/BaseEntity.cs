using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.Entities
{
    public class BaseEntity
    {
        public virtual object Id { get; set; } = Guid.NewGuid();
    }
}
