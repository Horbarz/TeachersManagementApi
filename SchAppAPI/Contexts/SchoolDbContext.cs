using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using SchAppAPI.Models;
using SchAppAPI.Models.Chat;
using SchAppAPI.Models.Lesson;

namespace SchAppAPI.Contexts
{
    public class SchoolDbContext : IdentityDbContext<User>
    {
        //public DbSet<Teacher> Teachers { get; set; }

        public SchoolDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LessonReport>().HasKey(lr => new { lr.LessonId, lr.TeacherId });
            builder.Entity<QuizReport>().HasKey(qr => new { qr.QuizId, qr.TeacherId });

            builder.Entity<QuizReport>().Property(qr => qr.QuizUserAnswers).HasConversion(
                qua => JsonConvert.SerializeObject(qua, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                qua => JsonConvert.DeserializeObject<IList<QuizUserAnswer>>(qua, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),

                new ValueComparer<IList<QuizUserAnswer>>(
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
                v => JsonConvert.DeserializeObject<IList<QuizUserAnswer>>(JsonConvert.SerializeObject(v)))

             );

            builder.Entity<Message>().HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Message>()
                .HasOne(x => x.Receipient)
                .WithMany()
                .HasForeignKey(x => x.ReceipientId)
                .OnDelete(DeleteBehavior.Restrict);


        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizReport> QuizReports { get; set; }
        public DbSet<LessonReport> LessonReports { get; set; }
        public DbSet<Message> Messages{ get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(cancellationToken));
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedOn = utcNow;
                            entry.Property("CreatedOn").IsModified = false;
                            break;
                        case EntityState.Added:
                            trackable.CreatedOn = utcNow;
                            trackable.UpdatedOn = utcNow;
                            break;
                    }
                }
            }
        }

    }
}
