//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Server.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class focuspoint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public focuspoint()
        {
            this.practicesessionsmain = new HashSet<practicesession>();
            this.members = new HashSet<member>();
            this.practicesessionssub = new HashSet<practicesession>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public string Description { get; set; }
        public string VideoURL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<practicesession> practicesessionsmain { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<member> members { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<practicesession> practicesessionssub { get; set; }
    }
}
