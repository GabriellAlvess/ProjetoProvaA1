namespace ProjetoProvaA1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Genres", "GameId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Genres", "GameId");
        }
    }
}
