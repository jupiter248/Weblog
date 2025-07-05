using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.SmsDtos
{
    public class AddSendModelDto
    {
        public required string Mobile { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }


    }
}