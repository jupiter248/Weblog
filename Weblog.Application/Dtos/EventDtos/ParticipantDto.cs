using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.EventDtos
{
    public class ParticipantDto
    {
        public int Id { get; set; }
        public required string Phone { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Username { get; set; }
        public required bool IsConfirmed { get; set; } = false;
    }
}