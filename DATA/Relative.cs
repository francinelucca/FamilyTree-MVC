//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FamilyTree2.DATA
{
    using System;
    using System.Collections.Generic;
    
    public partial class Relative
    {
        public int id { get; set; }
        public int PersonaId { get; set; }
        public int RelationshipId { get; set; }
        public System.DateTime createdAt { get; set; }
        public Nullable<System.DateTime> updatedAt { get; set; }
        public int RelativeId { get; set; }
    
        public virtual Persona Persona { get; set; }
        public virtual Relationship Relationship { get; set; }
        public virtual Persona RelatedTo { get; set; }
    }
}
