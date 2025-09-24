using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.JoinModels
{
    public class GuestTakingPart : TakingPart
    {
        public string GuestName { get; set; } = default!;
        public string GuestFamily { get; set; } = default!;
        public string GuestPhone { get; set; } = default!;
    }
}