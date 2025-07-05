using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.SmsDtos
{
    public class VerifySendModel
    {
        public required string Mobile { get; set; }
        public int TemplateId { get; set; }
        public required VerifySendParameterModel[] Parameters { get; set; }
    }
}