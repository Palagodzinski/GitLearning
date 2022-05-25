using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Usr_ID { get; set; }
        public string Usr_Name { get; set; }
        public string Usr_LastName { get; set; }
        public string Usr_Password { get; set; }
        public string Usr_Email { get; set; }
        public DateTime Usr_Created { get; set; }
        [ForeignKey("UserID")]
        public virtual ISet<Books>? Books { get; set; }
        public virtual ISet<DelayedBooks>? DelayedBooks { get; set; }
    }
}
