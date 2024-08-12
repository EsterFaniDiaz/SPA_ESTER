 
namespace ClassLibrary1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ClientesModel
    {
        public ClientesModel()
        {
            this.Reservas = new HashSet<Reservas>();
        }
         

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string nombre_cl { get; set; }

        [Required(ErrorMessage = "El Numero de Documento es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Numero de Documento no puede exceder los 50 caracteres.")]
        public string Numero_Documento { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public Nullable<int> teléfono_cl { get; set; }

        [Required(ErrorMessage = "La dirreción es obligatoria.")]
        [StringLength(100, ErrorMessage = "La dirreción no puede exceder los 100 caracteres.")]
        public string dirección_cl { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres.")]
        public string correo_cl { get; set; }

        public Nullable<int> id_usuario { get; set; }

        public virtual Usuarios Usuarios { get; set; }
        public virtual ICollection<Reservas> Reservas { get; set; }
    }
}
