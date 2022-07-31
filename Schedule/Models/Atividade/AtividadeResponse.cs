using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models.Atividade
{
    public class AtividadeResponse
    {
        public int Id { get; set; }
        public int Account_Id { get; set; }
        public string Nome { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
        public string Dia { get; set; }

    }
}
