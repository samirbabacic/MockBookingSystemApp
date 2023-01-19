using MockBookingSystem.Core;
using MockBookingSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.Entities
{
    public record Booking : BaseEntity
    {
        public override string Id { get { return Code; } }

        private string _code;
        public string Code
        {
            get
            {
                if (!string.IsNullOrEmpty(_code))
                    return _code;

                _code = RandomGenerator.GenerateAlpahnumericString(6);

                return _code;
            }
            private set { _code = value; }
        }
        public int? SleepTime { get; private set; } = RandomGenerator.GenerateInt(30, 60);

        public DateTime CreationTime { get; private set; } = DateTime.Now;
        public BookingType Type { get; set; }

        private BookingStatus _status = BookingStatus.Pending;
        public BookingStatus Status
        {
            get { return _status; }
            set
            {
                if (value == BookingStatus.Pending)
                {
                    _status = BookingStatus.Pending;
                    return;
                }

                SleepTime = null;
                _status = value;
            }
        }
    }
}
