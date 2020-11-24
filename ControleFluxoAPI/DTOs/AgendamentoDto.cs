
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFluxoAPI.DTOs
{
    public class AgendamentoDto
    {
        public int Id { get; set; }
        public int VagaId { get; set; }
        public int FornecedorId { get; set; }
        public string FornecedorNome { get; set; }
        public string PlacaVeiculo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }
}
