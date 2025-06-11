using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Models
{
    public class Medium
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public bool IsPrimary { get; set; } = false;
    }
}