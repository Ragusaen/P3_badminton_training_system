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
    
    public partial class feedback
    {
        public int MemberID { get; set; }
        public int PlaySessionID { get; set; }
        public Nullable<int> Ready { get; set; }
        public Nullable<int> Effort { get; set; }
        public Nullable<int> Challenge { get; set; }
        public Nullable<int> Absorb { get; set; }
        public string Good { get; set; }
        public string Bad { get; set; }
        public string FocusPoint { get; set; }
        public string Day { get; set; }
    
        public virtual member member { get; set; }
        public virtual playsession playsession { get; set; }
    }
}