using Schedule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models.Atividade
{
    public class AtividadeUpdateRequest
    {
        private string _nome;
        private int? _inicio;
        private int? _fim;

        public string Nome
        {
            get => _nome;
            set => _nome = replaceEmptyWithNull(value);
        }
        public int? Inicio 
        { 
            get => _inicio; 
            set => _inicio = value == null ? null : value;
        }
        public int? Fim
        {
            get => _fim;
            set => _fim = value == null ? null : value;
        }
        public DiaSemana? Dia { get; set; }

        //Para tornar o campo opcional
        private string replaceEmptyWithNull(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }


    }
}
