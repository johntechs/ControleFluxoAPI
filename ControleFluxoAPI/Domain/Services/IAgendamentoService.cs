using ControleFluxoAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFluxoAPI.Domain.Services
{
    public interface IAgendamentoService
    {
        Task<IEnumerable<Agendamento>> ListAsync();
        Task<Agendamento> FindByIdAsync(int id);
        Task AddAsync(Agendamento agendamento);
        void Update(Agendamento agendamento);
        void Remove(Agendamento agendamento);
        Task SaveChangesAsync();
    }
}
