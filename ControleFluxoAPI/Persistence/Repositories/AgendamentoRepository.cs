using ControleFluxoAPI.Domain.Models;
using ControleFluxoAPI.Domain.Repositories;
using ControleFluxoAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFluxoAPI.Persistence.Repositories
{
    public class AgendamentoRepository : BaseRepository, IAgendamentoRepository
    {
        public AgendamentoRepository(ControleFluxoDBContext context) : base(context)
        { }
        public async Task<IEnumerable<Agendamento>> ListAsync()
        {
            return await _context.Agendamentos.ToListAsync();
        }

        public async Task AddAsync(Agendamento agendamento)
        {
            await _context.Agendamentos.AddAsync(agendamento);
        }

        public async Task<Agendamento> FindByIdAsync(int id)
        {
            return await _context.Agendamentos.FindAsync(id);
        }

        public void Remove(Agendamento agendamento)
        {
            _context.Agendamentos.Remove(agendamento);
        }

        public void Update(Agendamento agendamento)
        {
            _context.Agendamentos.Update(agendamento);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ContainsByPeriodoAndVagaAsync(DateTime dataInicio, DateTime dataFim, int vagaId)
        {
            return await _context.Agendamentos
                    .AnyAsync(p => p.VagaId == vagaId && (p.DataInicio >= dataInicio && p.DataFim <= dataFim));
        }

        public async Task<int> CountByFornecedorAsync(int fornecedorId, DateTime dataInicio, DateTime dataFim)
        {
            return await _context.Agendamentos
                    .CountAsync(p => p.FornecedorId == fornecedorId && ((p.DataInicio <= dataInicio && p.DataFim >= dataInicio) || (p.DataInicio <= dataFim && p.DataFim >= dataFim)));
        }

        public async Task<int> CountByDataInicioAsync(DateTime dataInicio)
        {
            return await _context.Agendamentos
                    .CountAsync(p => p.DataInicio.Date >= dataInicio);
        }

        public async Task<bool> ContainsByPeriodoAndPlacaAsync(DateTime dataInicio, DateTime dataFim, String placaVeiculo)
        {
            return await _context.Agendamentos
                    .AnyAsync(p => p.PlacaVeiculo == placaVeiculo && (p.DataInicio >= dataInicio && p.DataFim <= dataFim));
        }

    }
}
