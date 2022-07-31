using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.Migrations
{
    public partial class Popular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Account(FirstName,LastName,Email,PasswordHash,AcceptTerms,Role,Verified,Created)
                                   VALUES('Admin','Admin','Admin@email.com','$2a$11$bNFNAWj2lL7/a0NNhTR3XOiNNcXy6.eMbWfGEEoaM.qZqiVpDWoIy',1,0,GETDATE(),GETDATE());");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Account");
        }
    }
}
