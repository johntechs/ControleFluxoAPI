using AutoMapper;
using ControleFluxoAPI.Domain.Models;
using ControleFluxoAPI.Domain.Services;
using ControleFluxoAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFluxoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly IMapper _mapper;
        public AgendamentoController(IAgendamentoService agendamentoService, IMapper mapper)
        {
            _agendamentoService = agendamentoService;
            _mapper = mapper;
        }

        // GET: api/Agendamento
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var agendamentos = await _agendamentoService.ListAsync();
            var agendamentosDto = _mapper.Map<IEnumerable<AgendamentoDto>>(agendamentos);

            return Ok(agendamentosDto);
        }

        // GET: api/Agendamento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgendamento(int id)
        {
            var agendamento = await _agendamentoService.FindByIdAsync(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            var agendamentoDto = _mapper.Map<AgendamentoDto>(agendamento);

            return Ok(agendamentoDto);
        }

        // POST: api/Agendamento
        [HttpPost]
        public async Task<IActionResult> AddAsync(AgendamentoDto agendamentoDto)
        {

            var agendamento = _mapper.Map<Agendamento>(agendamentoDto);

            await _agendamentoService.AddAsync(agendamento);
            await _agendamentoService.SaveChangesAsync();

            return Ok(agendamento);
            
            
        }

    }
}
