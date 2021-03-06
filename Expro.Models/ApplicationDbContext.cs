﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Expro.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.AccountNumber)
                .IsUnique();

            modelBuilder.Entity<UserLawArea>()
                .HasKey(bc => new { bc.UserID, bc.LawAreaID });
            modelBuilder.Entity<UserLawArea>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserLawAreas)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<UserLawArea>()
                .HasOne(bc => bc.LawArea)
                .WithMany(c => c.UserLawAreas)
                .HasForeignKey(bc => bc.LawAreaID);

            modelBuilder.Entity<DocumentLawArea>()
                .HasKey(bc => new { bc.DocumentID, bc.LawAreaID });
            modelBuilder.Entity<DocumentLawArea>()
                .HasOne(bc => bc.Document)
                .WithMany(b => b.DocumentLawAreas)
                .HasForeignKey(bc => bc.DocumentID);
            modelBuilder.Entity<DocumentLawArea>()
                .HasOne(bc => bc.LawArea)
                .WithMany(c => c.DocumentLawAreas)
                .HasForeignKey(bc => bc.LawAreaID);

            modelBuilder.Entity<LawArea>()
                .HasOne(p => p.Parent)
                .WithMany(b => b.Children)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CompanyLawArea>()
                .HasKey(bc => new { bc.CompanyID, bc.LawAreaID });
            modelBuilder.Entity<CompanyLawArea>()
                .HasOne(bc => bc.Company)
                .WithMany(b => b.CompanyLawAreas)
                .HasForeignKey(bc => bc.CompanyID);
            modelBuilder.Entity<CompanyLawArea>()
                .HasOne(bc => bc.LawArea)
                .WithMany(c => c.CompanyLawAreas)
                .HasForeignKey(bc => bc.LawAreaID);

            modelBuilder.Entity<QuestionLawArea>()
                .HasKey(bc => new { bc.QuestionID, bc.LawAreaID });
            modelBuilder.Entity<QuestionLawArea>()
                .HasOne(bc => bc.Question)
                .WithMany(b => b.QuestionLawAreas)
                .HasForeignKey(bc => bc.QuestionID);
            modelBuilder.Entity<QuestionLawArea>()
                .HasOne(bc => bc.LawArea)
                .WithMany(c => c.QuestionLawAreas)
                .HasForeignKey(bc => bc.LawAreaID);

            modelBuilder.Entity<QuestionAnswerComment>()
                .HasKey(bc => new { bc.QuestionAnswerID, bc.CommentID });
            modelBuilder.Entity<QuestionAnswerComment>()
                .HasOne(bc => bc.QuestionAnswer)
                .WithMany(b => b.Comments)
                .HasForeignKey(bc => bc.QuestionAnswerID);
            modelBuilder.Entity<QuestionAnswerComment>()
                .HasOne(bc => bc.Comment)
                .WithMany(c => c.QuestionAnswerComments)
                .HasForeignKey(bc => bc.CommentID);

            modelBuilder.Entity<DocumentLike>()
                .HasKey(bc => new { bc.DocumentID, bc.LikeID });
            modelBuilder.Entity<DocumentLike>()
                .HasOne(bc => bc.Document)
                .WithMany(b => b.DocumentLikes)
                .HasForeignKey(bc => bc.DocumentID);
            modelBuilder.Entity<DocumentLike>()
                .HasOne(bc => bc.Like)
                .WithMany(c => c.DocumentLikes)
                .HasForeignKey(bc => bc.LikeID);

            modelBuilder.Entity<QuestionAnswerLike>()
                .HasKey(bc => new { bc.QuestionAnswerID, bc.LikeID });
            modelBuilder.Entity<QuestionAnswerLike>()
                .HasOne(bc => bc.QuestionAnswer)
                .WithMany(b => b.QuestionAnswerLikes)
                .HasForeignKey(bc => bc.QuestionAnswerID);
            modelBuilder.Entity<QuestionAnswerLike>()
                .HasOne(bc => bc.Like)
                .WithMany(c => c.QuestionAnswerLikes)
                .HasForeignKey(bc => bc.LikeID);

          
                

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasOne(e => e.Avatar)
            //    .WithMany(c => c.UsersUsingThisAvatar);
            //modelBuilder.Entity<Attachment>()
            //    .HasMany(e => e.UsersUsingThisAvatar)
            //    .WithOne(c => c.Avatar);

            //modelBuilder.Entity<Gender>()
            //    .HasMany(c => c.Users)
            //    .WithOne(e => e.Gender);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(c => c.Users)
            //    .WithOne(e => e.Gender);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        //public DbSet<Post> Posts { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<LawArea> LawAreas { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ExpertEducation> ExpertEducations { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<DocumentStatus> DocumentStatuses { get; set; }
        public DbSet<UserPurchasedDocument> UserPurchasedDocuments { get; set; }
        public DbSet<ClickTransaction> ClickTransactions { get; set; }
        public DbSet<WithdrawRequest> WithdrawRequests { get; set; }
        public DbSet<WithdrawRequestStatus> WithdrawRequestStatuses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyStatus> CompanyStatuses { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<ResumeStatus> ResumeStatuses { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<VacancyStatus> VacancyStatuses { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<RatingUpdate> RatingUpdates { get; set; }
        public DbSet<LocalizationShort> LocalizationShorts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackStatus> FeedbackStatuses { get; set; }
    }
}
