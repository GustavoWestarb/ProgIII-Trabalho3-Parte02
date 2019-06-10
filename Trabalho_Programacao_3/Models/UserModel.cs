using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Trabalho_Programacao_3.Models
{
    public class UserModel
    {
        [Key]
        [Column("cd_user")]
        public long ID { get; set; }

        [Column("nm_email")]
        [Required(ErrorMessage = "Informe o seu e-mail")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Informe um e-mail válido...")]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("nm_password")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100)]
        public string Password { get; set; }

        [JsonIgnore]
        public ICollection<PadModel> Pads { get; set; }
    }
}