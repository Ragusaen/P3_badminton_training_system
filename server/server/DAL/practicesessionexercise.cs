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
    
    public partial class practicesessionexercise
    {
        public int ExerciseID { get; set; }
        public int PracticeSessionPlaySessionID { get; set; }
        public int ExerciseIndex { get; set; }
    
        public virtual exercise exercise { get; set; }
        public virtual practicesession practicesession { get; set; }
    }
}