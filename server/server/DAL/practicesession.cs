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
    
    public partial class practicesession
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public practicesession()
        {
            this.practicesessionexercises = new HashSet<practicesessionexercise>();
            this.focuspoints = new HashSet<focuspoint>();
        }
    
        public int PlaySessionID { get; set; }
        public int YearPlanSectionID { get; set; }
        public int TrainerID { get; set; }
    
        public virtual member member { get; set; }
        public virtual playsession playsession { get; set; }
        public virtual yearplansection yearplansection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<practicesessionexercise> practicesessionexercises { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<focuspoint> focuspoints { get; set; }
    }
}
