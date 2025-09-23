using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Errors.Event
{
    public class EventErrorCodes
    {
        public const string EventNotFound = "EVENT_NOT_FOUND";
        public const string EventTitleRequired = "EVENT_TITLE_REQUIRED";
        public const string EventTitleMaxLength = "EVENT_TITLE_MAX_LENGTH";
        public const string EventDescriptionRequired = "EVENT_DESCRIPTION_REQUIRED";
        public const string EventDescriptionMaxLength = "EVENT_DESCRIPTION_MAX_LENGTH";
        public const string EventContextRequired = "EVENT_CONTEXT_REQUIRED";
        public const string EventPlaceRequired = "EVENT_PLACE_REQUIRED";
        public const string EventPlaceMaxLength = "EVENT_PLACE_MAX_LENGTH";
        public const string EventCapacityIsFull = "EVENT_CAPACITY_IS_FULL";
        public const string EventAboveTitleRequired = "EVENT_ABOVE_TITLE_REQUIRED";
        public const string EventAboveTitleMaxLength = "EVENT_ABOVE_TITLE_MAX_LENGTH";
        public const string EventBelowTitleRequired = "EVENT_BELOW_TITLE_REQUIRED";
        public const string EventBelowTitleMaxLength = "EVENT_BELOW_TITLE_MAX_LENGTH";

    }
}