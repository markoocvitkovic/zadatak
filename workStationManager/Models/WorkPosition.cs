using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workStationManager.Models
{
    public class WorkPosition
    {
        [Key]
        public int Id { get; set; }
        public required string PositionName { get; set; }
        public string? PositionDescription { get; set; }
        public List<UserWorkPosition> Users { get; set; } = [];
    }
}
