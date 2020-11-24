using ControleFluxoAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFluxoAPI.Domain.Repositories
{
    public interface IAgendamentoRepository
    {

        Task<IEnumerable<Agendamento>> ListAsync();
        Task AddAsync(Agendamento agendamento);
        Task<Agendamento> FindByIdAsync(int id);
        void Update(Agendamento agendamento);
        void Remove(Agendamento agendamento);
        Task SaveChangesAsync();
        Task<bool> ContainsByPeriodoAndVagaAsync(DateTime dataInicio, DateTime dataFim, int vagaId);
        Task<int> CountByDataInicioAsync(DateTime dataInicio);
        Task<bool> ContainsByPeriodoAndPlacaAsync(DateTime dataInicio, DateTime dataFim, string placaVeiculo);
        Task<int> CountByFornecedorAsync(int fornecedorId, DateTime dataInicio, DateTime dataFim);
    }
}
