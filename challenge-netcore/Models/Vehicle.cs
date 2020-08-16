using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace challenge_netcore.Models
{
    public class Vehicle
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(8)]
        public string LicensePlate { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Doors { get; set; }
        [Required]
        public string Owner { get; set; }
    }
}
