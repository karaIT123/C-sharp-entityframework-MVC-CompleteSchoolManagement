//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompleteSchoolManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Material
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public string Movie { get; set; }
        public string Laboratory { get; set; }
        public string Exercise { get; set; }
        public string Solution { get; set; }
        public int CoursesId { get; set; }
    
        public virtual Courses Courses { get; set; }
    }
}
