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
    
    public partial class position
    {
        public int MemberID { get; set; }
        public int TeamMatchPlaySessionID { get; set; }
        public int Type { get; set; }
        public int Order { get; set; }
        public bool IsExtra { get; set; }
    
        public virtual member member { get; set; }
        public virtual teammatch teammatch { get; set; }
    }
}
