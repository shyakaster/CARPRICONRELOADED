using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Objects;
using Capricon.Model;

namespace Capricon.Model
{
    public class CapriconContext : DbContext
    {
        public CapriconContext() : base("Capricon")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<AgentMessage> AgentMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId)
                .Map(m =>
                    {
                        m.MapInheritedProperties();
                        m.ToTable("Users");
                    });

            modelBuilder.Entity<Agent>()
                .HasKey(u => u.AgentId)
                .Map(m =>
                    {
                        m.MapInheritedProperties();
                        m.ToTable("Agents");
                    });
        }

        public int Save()
        {
            return this.SaveChanges();
        }
    }
}
