using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workStationManager.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public required string RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public List<User> Users { get; set; } = [];
    }

}
