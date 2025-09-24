using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.EventDtos
{
    public class AddGuestTakinPartDto
    {
        public string GuestName { get; set; } = default!;
        public string GuestFamily { get; set; } = default!;
        public string GuestPhone { get; set; } = default!;

    }
}