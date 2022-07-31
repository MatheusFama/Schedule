
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Entities
{
    [Table("Atividade")]

    public partial class Atividade
    {
        public int Id { get; set; }
        public int Account_Id { get; set; }
        [MaxLength(250)]
        public string Nome { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
        public DiaSemana Dia { get; set; }

        public virtual Account Account { get; set; }
    }
}
