using ControleFluxoAPI.Domain.Models;
using ControleFluxoAPI.Domain.Repositories;
using ControleFluxoAPI.Domain.Services;
using ControleFluxoAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFluxoAPI.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        public AgendamentoService(IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }

        public async Task<IEnumerable<Agendamento>> ListAsync()
        {
            return await _agendamentoRepository.ListAsync();
        }

        public async Task<Agendamento> FindByIdAsync(int id)
        {
            return await _agendamentoRepository.FindByIdAsync(id);
        }

        public async Task AddAsync(Agendamento agendamento)
        {
            if (await _agendamentoRepository.ContainsByPeriodoAndPlacaAsync(agendamento.DataInicio, agendamento.DataFim, agendamento.PlacaVeiculo))
            {//Validar se a placa do veículo já está ocupando uma vaga naquele horário
                throw new BusinessException("O Veículo informado já está ocupando uma vaga neste mesmo horário.");
            }

            if (await _agendamentoRepository.ContainsByPeriodoAndVagaAsync(agendamento.DataInicio, agendamento.DataFim, agendamento.VagaId))
            {//Validar se a vaga está ocupada no intervalo de horário 
                throw new BusinessException("A Vaga informada já está ocupada no período solicitado.");
            }

            if (await _agendamentoRepository.CountByDataInicioAsync(agendamento.DataInicio) >= 12)
            {//Validar limite máximo diário de veículos agendados
                throw new BusinessException("O limite diário de 12 veículos agendados para recebimento foi atingido.");
            }

            if (await _agendamentoRepository.CountByFornecedorAsync(agendamento.FornecedorId, agendamento.DataInicio, agendamento.DataFim) >= 2)
            {//Validar se o fornecedor já tem duas vagas ocupadas no mesmo horário
                throw new BusinessException("O limite de 2 vagas ocupadas pelo mesmo fornecedor simultaneamente foi atingido.");
            }

            await _agendamentoRepository.AddAsync(agendamento);
        }

        public async Task SaveChangesAsync()
        {
            await _agendamentoRepository.SaveChangesAsync();
        }

        public void Update(Agendamento agendamento)
        {
            throw new NotImplementedException();
        }

        public void Remove(Agendamento agendamento)
        {
            throw new NotImplementedException();
        }
    }
}
