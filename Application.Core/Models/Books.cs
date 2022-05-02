using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class Books
    {
        [Key]
        public int Bks_ID { get; set; }
        public string Bks_Title { get; set; }
        public string Bks_Author { get; set; }
        public DateTime? Bks_ReturnDate { get; set; }
        public virtual UserModel? User { get; set; }
    }
}
