using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workStationManager.Models
{
    public class UserWorkPosition
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public int WorkPositionId { get; set; }
        public required WorkPosition WorkPosition { get; set; }
        public required string ProductName { get; set; }
        public required DateTime WorkDate { get; set; }
    }
}
