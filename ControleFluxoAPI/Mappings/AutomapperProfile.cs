using AutoMapper;
using ControleFluxoAPI.Domain.Models;
using ControleFluxoAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFluxoAPI.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Agendamento, AgendamentoDto>()
                .ForMember(a => a.FornecedorNome, opt => opt.MapFrom(src => src.Fornecedor.Nome));
            CreateMap<AgendamentoDto, Agendamento>();
        }
    }
}
