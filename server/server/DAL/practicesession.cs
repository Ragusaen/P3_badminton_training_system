//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace server.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class practicesession
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public practicesession()
        {
            this.practicesessionexercises = new HashSet<practicesessionexercise>();
            this.subfocuspoints = new HashSet<focuspoint>();
        }
    
        public int PlaySessionID { get; set; }
        public Nullable<int> TrainerID { get; set; }
        public Nullable<int> MainFocusPointID { get; set; }
        public int TeamID { get; set; }
    
        public virtual focuspoint mainfocuspoint { get; set; }
        public virtual member trainer { get; set; }
        public virtual playsession playsession { get; set; }
        public virtual practiceteam practiceteam { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<practicesessionexercise> practicesessionexercises { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<focuspoint> subfocuspoints { get; set; }
    }
}
