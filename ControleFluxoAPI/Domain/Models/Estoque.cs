using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFluxoAPI.Domain.Models
{
    public class Estoque
    {
        [Key]
        public int Id { get; set; }

        [InverseProperty("Estoque")]
        public virtual ICollection<Vaga> Vagas { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string nome { get; set; }
    }
}
