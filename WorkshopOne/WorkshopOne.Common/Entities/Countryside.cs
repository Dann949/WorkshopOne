using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkshopOne.Common.Entities
{
  public  class Countryside
    {
        public int Id{ get; set; }

        [MaxLength (50)]
        [Required]
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }
        [DisplayName("Districts Number")]
        public int DistrictsNumber => Districts == null ? 0 : Districts.Count;

    }
}
