using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TDS.IdentityService.Infrastructure.Persistence.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserName { get; set; }

        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
