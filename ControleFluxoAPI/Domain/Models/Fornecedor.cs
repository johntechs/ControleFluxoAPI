using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFluxoAPI.Domain.Models
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Nome { get; set; }

        [InverseProperty("Fornecedor")]
        public virtual ICollection<Agendamento> Agendamentos { get; set; }


    }
}
