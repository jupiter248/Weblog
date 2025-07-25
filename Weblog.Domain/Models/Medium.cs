using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Enums;

namespace Weblog.Domain.Models
{
    public class Medium
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public MediumType MediumType { get; set; }
        public EntityType EntityType { get; set; }
        public required int EntityId { get; set; }
        public string? UserId{ get; set; }
        public AppUser? AppUser{ get; set; }
        public bool IsPrimary { get; set; } = false;
        public bool IsOnPoster { get; set; } = false;

    }
}