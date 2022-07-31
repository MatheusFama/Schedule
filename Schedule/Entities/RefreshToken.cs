using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Schedule.Entities
{
    [Owned]
    public class RefreshToken
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        [MaxLength(500)]
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        [MaxLength(500)]
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        [MaxLength(500)]
        public string RevokedByIp { get; set; }
        [MaxLength(500)]
        public string ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
