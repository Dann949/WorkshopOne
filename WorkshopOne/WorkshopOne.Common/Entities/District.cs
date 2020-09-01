using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkshopOne.Common.Entities
{
   public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<church> Churches { get; set; }

        [DisplayName("Chusches Number")]
        public int ChurchesNumber => Churches == null ? 0 : Churches.Count;

        [NotMapped]
        public int IdCountryside { get; set; }
    }
}
