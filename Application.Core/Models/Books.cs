using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    [Table("Books")]
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Bks_ID { get; set; }
        public string Bks_Title { get; set; }
        public string Bks_Author { get; set; }
        public virtual UserModel Users { get; set; }
    }
}
