using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.EventDtos
{
    public class ParticipantDto
    {
        public required string Phone { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsConfirmed { get; set; } = false;
    }
}