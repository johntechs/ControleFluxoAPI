using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ControleFluxoAPI.Domain.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Vaga")]
        public int VagaId { get; set; }
        public virtual Vaga Vaga { get; set; }

        [ForeignKey("Fornecedor")]
        public int FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }

        [Column(TypeName = "nvarchar(7)")]
        public string PlacaVeiculo { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime DataInicio { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime DataFim { get; set; }
    }
}
