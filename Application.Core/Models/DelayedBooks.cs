using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Models
{
    public class DelayedBooks
    {
        [Key]
        public int Db_Id { get; set; }

        public virtual UserModel User { get; set; }

        public virtual Books Book { get; set; }
    }
}
