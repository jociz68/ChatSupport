namespace DataDbModel
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ChatModel : DbContext
    {
        public ChatModel()
            : base("name=ChatModel")
        {
            base.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }
    }


}