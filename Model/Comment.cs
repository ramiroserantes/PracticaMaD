//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMad.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public long commentId { get; set; }
        public string commentDescription { get; set; }
        public System.DateTime commentDate { get; set; }
        public string userName { get; set; }
        public long userId { get; set; }
        public long photoId { get; set; }
    
        public virtual Photo Photo { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
