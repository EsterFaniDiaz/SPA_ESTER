using System;
using System.ComponentModel.DataAnnotations;


namespace ClassLibrary1.Models
{
    public partial class UsuarioClientesModel
    { 

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

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [StringLength(100, ErrorMessage = "El usuario no puede exceder los 100 caracteres.")]
        public string usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contraseña { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

    }
}
