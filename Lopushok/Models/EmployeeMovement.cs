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
    
    public partial class EmployeeMovement
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public System.DateTime DateTime { get; set; }
        public Nullable<System.TimeSpan> StartJobTime { get; set; }
        public Nullable<System.TimeSpan> EndJobTime { get; set; }
        public bool IsBreak { get; set; }
        public Nullable<System.TimeSpan> BreakTime { get; set; }
        public Nullable<int> InTurnikettId { get; set; }
        public Nullable<int> OutTurniketId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Turniket Turniket { get; set; }
        public virtual Turniket Turniket1 { get; set; }
    }
}