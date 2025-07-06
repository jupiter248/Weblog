using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.SmsDtos
{
    public class ConsentSmsParameterModel
    {
        public required string Name { get; set; }
        public required string Value { get; set; }
    }
}