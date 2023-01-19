using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.Entities
{
    public record BaseEntity
    {
        public virtual string Id { get; init; } = Guid.NewGuid().ToString();
    }
}
