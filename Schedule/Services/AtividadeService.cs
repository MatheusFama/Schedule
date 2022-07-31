using AutoMapper;
using Schedule.Entities;
using Schedule.Helpers;
using Schedule.Models.Atividade;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.Services
{
    public interface IAtividadeService
    {
        IEnumerable<AtividadeResponse> GetAll();
        AtividadeResponse GetById(int account_Id, int atividade_Id);

        AtividadeResponse Create(int account_Id, AtividadeCreateRequest request);
        AtividadeResponse Update(int account_id, int atividade_id, AtividadeUpdateRequest request);
        void Delete(int account_id, int id);

    }
    public class AtividadeService : IAtividadeService
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public AtividadeService(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<AtividadeResponse> GetAll()
        {
            var atividades = _mapper.Map<IList<AtividadeResponse>>(_context.Atividades);
            return atividades;
        }

        public AtividadeResponse GetById(int account_Id, int atividade_id)
        {
            return _mapper.Map<AtividadeResponse>(byId(atividade_id, account_Id));
        }

        public AtividadeResponse Create(int account_id, AtividadeCreateRequest request)
        {
            var atividade = _mapper.Map<Atividade>(request);

            if (!isAtividadeValida(account_id, atividade))
                throw new AppException("Atividade inválida");


            atividade.Account_Id = account_id;
            _context.Atividades.Add(atividade);
            _context.SaveChanges();

            return _mapper.Map<AtividadeResponse>(atividade);
        }

        public AtividadeResponse Update(int account_id, int atividade_id, AtividadeUpdateRequest request)
        {
            var atividade = byId(atividade_id, account_id);

            if (!isAtividadeValida(account_id, atividade))
                throw new AppException("Atividade inválida");

            _mapper.Map(request, atividade);

            _context.Atividades.Update(atividade);
            _context.SaveChanges();

            return _mapper.Map<AtividadeResponse>(atividade);

        }

        public void Delete(int account_id, int id)
        {
            _context.Atividades.Remove(_context.Atividades.FirstOrDefault(a => a.Account_Id == account_id && a.Id == id));
        }

        private Atividade byId(int id, int account_id)
        {
            var atividade = _context.Atividades.FirstOrDefault(act => act.Id == id && act.Account_Id == account_id);
            if (atividade == null)
                throw new KeyNotFoundException("Atividade não encontrada");
            return atividade;
        }

        private bool isAtividadeValida(int account_id, Atividade atividade)
        {
            //Verificar se Inicio < Fim
            if (atividade.Fim < atividade.Inicio)
                return false;



            //Verificar se Inicio ou Fim estão contidos em algum intervalo
            //Ini <= Inicio <= Fim OU Ini <= Final <= Fim 
            if (_context.Atividades.Any(act => act.Account_Id == account_id
                                   && act.Id != atividade.Id
                                   && act.Dia == atividade.Dia
                                   && ((act.Inicio <= atividade.Inicio && atividade.Inicio <= act.Fim) || (act.Inicio <= atividade.Fim && atividade.Fim <= act.Fim)))
                )
                return false;



            //Verificar se Inicio ou Fim contém algum intervalo
            //Inicio <= Ini && Final >= Fim 
            if (_context.Atividades.Any(act => act.Account_Id == account_id
                               && act.Id != atividade.Id
                               && act.Dia == atividade.Dia
                               && (atividade.Inicio <= act.Inicio && atividade.Fim >= act.Fim))
            )
                return false;

            return true;
        }

    }
}
