//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BibliotecaClases
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservas()
        {
            this.Facturas = new HashSet<Facturas>();
        }
    
        public int id_reservas { get; set; }
        public Nullable<int> id_empleados { get; set; }
        public Nullable<int> id_clientes { get; set; }
        public Nullable<int> id_metodos_pg { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual Empleados Empleados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facturas> Facturas { get; set; }
        public virtual Metodos_Pago Metodos_Pago { get; set; }
    }
}
