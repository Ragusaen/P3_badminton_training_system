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
    
    public partial class teammatch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public teammatch()
        {
            this.positions = new HashSet<position>();
        }
    
        public int PlaySessionID { get; set; }
        public Nullable<int> CaptainID { get; set; }
        public string OpponentName { get; set; }
        public int League { get; set; }
        public int LeagueRound { get; set; }
        public int TeamIndex { get; set; }
        public int Season { get; set; }
    
        public virtual member captain { get; set; }
        public virtual playsession playsession { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<position> positions { get; set; }
    }
}
