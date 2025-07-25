using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.SmsDtos
{
    public class AddConsentSmsDto
    {
        [Phone]
        public required string Mobile { get; set; }
        public required  string Purpose { get; set; } // login or register
    }
}