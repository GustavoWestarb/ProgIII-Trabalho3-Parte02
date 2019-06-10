using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Trabalho_Programacao_3.Models
{
    public class PadModel
    {
        [Key]
        [Column("cd_pad")]
        public long ID { get; set; }

        [Column("nm_product")]
        public string Product { get; set; }

        [Column("vl_product")]
        public float Value { get; set; }

        [ForeignKey("UserModel")]
        [Column("cd_user")]
        [Required(ErrorMessage = "É preciso vincular um usuário com esta comanda")]
        public long UserModelRefId { get; set; }

        [JsonIgnore]
        public UserModel UserModel { get; set; }
    }
}