using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors.Event
{
    public class EventErrorCodes
    {
        public const string EventNotFound = "EVENT_NOT_FOUND";
        public const string EventCapacityIsFull = "EVENT_CAPACITY_IS_FULL";

    }
}