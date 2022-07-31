using Schedule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models.Atividade
{
    public class AtividadeCreateRequest
    {
        public string Nome { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
        public DiaSemana Dia { get; set; }
    }
}
