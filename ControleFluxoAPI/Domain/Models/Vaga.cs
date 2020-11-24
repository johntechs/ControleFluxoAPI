using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ControleFluxoAPI.Domain.Models
{
    public class Vaga
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "bit")]
        public bool IsOcupada { get; set; }
        
        [ForeignKey("Estoque")]
        public int EstoqueId { get; set; }
        public virtual Estoque Estoque { get; set; }

        [InverseProperty("Vaga")]
        public virtual ICollection<Agendamento> Agendamentos { get; set; }

    }
}
