using AutoMapper;
using Schedule.Entities;
using Schedule.Models.Accounts;
using Schedule.Models.Atividade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Atividade, AtividadeResponse>()
               .ForMember(dest => dest.Inicio, m => m.MapFrom(source => DateTime.Now.Date.AddMinutes(source.Inicio).ToString("HH:mm")))
               .ForMember(dest => dest.Fim, m => m.MapFrom(source => DateTime.Now.Date.AddMinutes(source.Fim).ToString("HH:mm")));

            CreateMap<Account, AccountResponse>();

            CreateMap<Account, AccountAuthenticateResponse>();

            CreateMap<AccountRegisterRequest, Account>();

            CreateMap<AccountCreateRequest, Account>();

            CreateMap<AccountUpdateRequest, Account>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }
                ));

            CreateMap<AtividadeCreateRequest, Atividade>();
            CreateMap<AtividadeUpdateRequest, Atividade>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        if (prop == null) return false;
                        if(prop.GetType().IsValueType )
                        //if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;
                        if (x.DestinationMember.Name == "Dia" && src.Dia == null) return false;
                        if (x.DestinationMember.Name == "Inicio" && !src.Inicio.HasValue) return false;
                        if (x.DestinationMember.Name == "Fim" && !src.Fim.HasValue) return false;


                        return true;
                    }
                ));

        }
    }
}
