using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkshopOne.Common.Entities
{
   public class church
    {

        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public String Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int IdDistrict { get; set; }
    }
}
