using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrisHub.Models
{
    public class Event
    {
        public int Id { get; set; }

        [MaxLength(6)]
        public string Code { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = "Decimal(18, 2)")]
        public decimal Fees { get; set; }

        [MaxLength(15)]
        public int SeatsFilled { get; set; }

        [MaxLength(200)]
        public string Logo { get; set; } = string.Empty;
    }
}
