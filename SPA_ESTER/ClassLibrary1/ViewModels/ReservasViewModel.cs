using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.ViewModels
{
    public partial class ReservasViewModel
    {

        public Nullable<int> id_empleados { get; set; }
        public Nullable<int> id_clientes { get; set; }
        public Nullable<int> id_metodos_pg { get; set; }
        public Nullable<int> id_servicios { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Debe ser una fecha válida.")]
        public DateTime FechaHora { get; set; }
        public Clientes Cliente { get; set; }
        public Empleados Empleado { get; set; }
        public List<Servicios> Servicios { get; set; }
    }
}
