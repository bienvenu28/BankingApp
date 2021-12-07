using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace BankingApp.Logic.Model
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Column("firstName")]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Column("lastName")]
        [StringLength(100)]
        public string LastName { get; set; }
        [Column("amount")]
        public double Amount { get; set; }
        [Column("pin")]
        public string Pin { get; set; }
        [Column("isBlocked")]
        public bool IsBlocked { get; set; }

        public override string ToString()
        {
            return "Id: " + this.Id + " FirstName: " + this.FirstName + " LastName: " + this.LastName
                   + " Amount: " + this.Amount + " Pin: " + this.Pin + " isBlocked: " + this.IsBlocked;
        }
    }
}