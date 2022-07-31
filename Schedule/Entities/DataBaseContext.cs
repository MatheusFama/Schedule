using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Entities
{
    /// <summary>
    /// Classe responsável por instanciar conexao com banco de dados
    /// </summary>
    public class DataBaseContext : DbContext
    {
        private readonly IConfiguration Configuration;

        public DataBaseContext(IConfiguration configurarion, DbContextOptions<DataBaseContext> options) : base(options)
        {
            Configuration = configurarion;
        }
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Atividades)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.Account_Id);
        }

    }
}
