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
    
    public partial class SaleHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AgentId { get; set; }
        public System.DateTime DateTime { get; set; }
        public decimal Cost { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual Product Product { get; set; }
    }
}