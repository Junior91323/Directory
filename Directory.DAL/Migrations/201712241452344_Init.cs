namespace Directory.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        FullName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Uid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
