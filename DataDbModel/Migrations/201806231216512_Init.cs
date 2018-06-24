namespace DataDbModel.Migrations
{
    using Helper;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        AnswerTime = c.DateTime(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        SessionId = c.String(),
                        AnswerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId)
                .Index(t => t.AnswerId);

            var adminPwdHash = PasswordHash.GenerateSHA256Hash("admin");

            Sql("insert into Services (Login, Password) values ('admin', '" + adminPwdHash + "')");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Questions", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Answers", "ServiceId", "dbo.Services");
            DropIndex("dbo.Questions", new[] { "AnswerId" });
            DropIndex("dbo.Answers", new[] { "ServiceId" });
            DropTable("dbo.Questions");
            DropTable("dbo.Services");
            DropTable("dbo.Answers");
        }
    }
}
