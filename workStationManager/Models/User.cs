using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workStationManager.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public List<UserWorkPosition> WorkPositions { get; set; } = [];
        public string DisplayPosition =>
            WorkPositions.FirstOrDefault() is UserWorkPosition uwp
                ? $"{uwp.WorkPosition?.PositionName ?? "No Position"} - {uwp.WorkDate.ToString("d")}"
                : "No Position";
    }

}
