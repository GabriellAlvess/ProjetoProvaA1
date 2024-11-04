namespace ProjetoProvaA1.Migrations
{
    using ProjetoProvaA1.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class InitialMigration2 : DbMigration
    {
        public override void Up()
        {
            if (!TableExists("AspNetUsers"))
            {
                CreateTable(
                    "dbo.AspNetUsers",
                    c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                    .PrimaryKey(t => t.Id)
                    .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            }

            // Criação de outras tabelas...
        }

        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.AspNetUsers");

            // Remoção de outras tabelas...
        }

        private bool TableExists(string tableName)
        {
            using (var context = new ApplicationDbContext())
            {
                var query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
                var count = context.Database.SqlQuery<int>(query).Single();
                return count > 0;
            }
        }
    }
}
