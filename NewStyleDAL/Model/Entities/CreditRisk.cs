using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Base;
using Model.Entities.Owned;


namespace Model.Entities
{
    [Table("CreditRisks", Schema = "dbo")]
    public partial class CreditRisk : BaseEntity
    {
        public Person PersonalInformation { get; set; } = new();

        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(Customer.CreditRisks))]
        public Customer? CustomerNavigation { get; set; }
    }
}
