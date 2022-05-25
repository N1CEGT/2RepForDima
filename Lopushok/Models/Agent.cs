//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lopushok.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            this.SaleHistories = new HashSet<SaleHistory>();
            this.SalePointAgents = new HashSet<SalePointAgent>();
            this.SupplyPriorityHistories = new HashSet<SupplyPriorityHistory>();
        }
    
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string CompanyName { get; set; }
        public int AgentTypeId { get; set; }
        public string Address { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string DirectorFullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] Logo { get; set; }
        public int PriorityInSupply { get; set; }
    
        public virtual AgentType AgentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleHistory> SaleHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalePointAgent> SalePointAgents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplyPriorityHistory> SupplyPriorityHistories { get; set; }
    }
}