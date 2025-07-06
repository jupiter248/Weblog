using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Dtos.SmsDtos
{
    public class ConsentSmsModel
    {
        public int Id { get; set; }
        public required string Mobile { get; set; }
        public int TemplateId { get; set; }
        public required ConsentSmsParameterModel[] Parameters { get; set; }
    }
}