using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperMatchNS
{
    public class DeveloperMatch
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid OrganizationUId { get; set; }
        public Guid DeveloperUId { get; set; }
        public DateTime Date { get; set; }
    }
}
