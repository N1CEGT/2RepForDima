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
    
    public partial class MaterialInStorageHistory
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int StorageId { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public int Count { get; set; }
    
        public virtual Material Material { get; set; }
        public virtual Storage Storage { get; set; }
    }
}
