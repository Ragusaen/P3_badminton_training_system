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
    
    public partial class ranklist
    {
        public int MemberID { get; set; }
        public int MixPoints { get; set; }
        public int SinglesPoints { get; set; }
        public int DoublesPoints { get; set; }
        public int LevelPoints { get; set; }
        public int Level { get; set; }
        public int AgeGroup { get; set; }
    
        public virtual member member { get; set; }
    }
}
