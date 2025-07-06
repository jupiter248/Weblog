using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Models
{
    public class VerificationCode
    {
        public int Id { get; set; }
        public required string Phone { get; set; }
        public required string Code { get; set; }
        public required DateTimeOffset ExpiresAt { get; set; }
        public required string Purpose { get; set; } // e.g. "register", "login"
    }
}