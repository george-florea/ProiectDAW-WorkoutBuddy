using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Backend.Entities;

namespace Backend.DataAccess
{
    public partial class WorkoutBuddyDBContext : DbContext
    {
        public WorkoutBuddyDBContext()
        {
        }

        public WorkoutBuddyDBContext(DbContextOptions<WorkoutBuddyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<ExerciseType> ExerciseTypes { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<MuscleGroup> MuscleGroups { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Reason> Reasons { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Split> Splits { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserExercise> UserExercises { get; set; } = null!;
        public virtual DbSet<UserExercisePr> UserExercisePrs { get; set; } = null!;
        public virtual DbSet<UserExerciseSet> UserExerciseSets { get; set; } = null!;
        public virtual DbSet<UserPointsHistory> UserPointsHistories { get; set; } = null!;
        public virtual DbSet<UserSplit> UserSplits { get; set; } = null!;
        public virtual DbSet<UserWeightHistory> UserWeightHistories { get; set; } = null!;
        public virtual DbSet<UserWorkout> UserWorkouts { get; set; } = null!;
        public virtual DbSet<Workout> Workouts { get; set; } = null!;
        public virtual DbSet<WorkoutExercise> WorkoutExercises { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:wbfea.database.windows.net,1433;Initial Catalog=workoutBuddy;Persist Security Info=False;User ID=wbAdmin;Password=Copernic@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => new { e.Idcomment, e.Idsplit });

                entity.HasIndex(e => e.Idcomment, "IX_Comments")
                    .IsUnique();

                entity.Property(e => e.Idcomment).HasColumnName("IDComment");

                entity.Property(e => e.Idsplit).HasColumnName("IDSplit");

                entity.Property(e => e.CommentText).HasMaxLength(255);

                entity.Property(e => e.IdparentComm).HasColumnName("IDParentComm");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.HasOne(d => d.IdparentCommNavigation)
                    .WithMany(p => p.InverseIdparentCommNavigation)
                    .HasPrincipalKey(p => p.Idcomment)
                    .HasForeignKey(d => d.IdparentComm)
                    .HasConstraintName("FK_Comments_Comments");

                entity.HasOne(d => d.IdsplitNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Idsplit)
                    .HasConstraintName("FK_Comments_Split");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => e.Idexercise);

                entity.ToTable("Exercise");

                entity.Property(e => e.Idexercise)
                    .ValueGeneratedNever()
                    .HasColumnName("IDExercise");

                entity.Property(e => e.Idimage).HasColumnName("IDImage");

                entity.Property(e => e.Idtype).HasColumnName("IDType");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.IdimageNavigation)
                    .WithMany(p => p.Exercises)
                    .HasForeignKey(d => d.Idimage)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Exercise_Images");

                entity.HasOne(d => d.IdtypeNavigation)
                    .WithMany(p => p.Exercises)
                    .HasForeignKey(d => d.Idtype)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exercise_ExerciseTypes");

                entity.HasMany(d => d.Idgroups)
                    .WithMany(p => p.Idexercises)
                    .UsingEntity<Dictionary<string, object>>(
                        "ExerciseMuscleGroup",
                        l => l.HasOne<MuscleGroup>().WithMany().HasForeignKey("Idgroup").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ExerciseMuscleGroup_MuscleGroups"),
                        r => r.HasOne<Exercise>().WithMany().HasForeignKey("Idexercise").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ExerciseMuscleGroup_Exercise"),
                        j =>
                        {
                            j.HasKey("Idexercise", "Idgroup");

                            j.ToTable("ExerciseMuscleGroup");

                            j.IndexerProperty<Guid>("Idexercise").HasColumnName("IDExercise");

                            j.IndexerProperty<int>("Idgroup").HasColumnName("IDGroup");
                        });
            });

            modelBuilder.Entity<ExerciseType>(entity =>
            {
                entity.HasKey(e => e.Idtype);

                entity.Property(e => e.Idtype).HasColumnName("IDType");

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.Idimg);

                entity.Property(e => e.Idimg)
                    .ValueGeneratedNever()
                    .HasColumnName("IDImg");
            });

            modelBuilder.Entity<MuscleGroup>(entity =>
            {
                entity.HasKey(e => e.Idgroup);

                entity.Property(e => e.Idgroup).HasColumnName("IDGroup");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Idpermission);

                entity.Property(e => e.Idpermission)
                    .ValueGeneratedNever()
                    .HasColumnName("IDPermission");
            });

            modelBuilder.Entity<Reason>(entity =>
            {
                entity.ToTable("Reason");

                entity.Property(e => e.ReasonId).ValueGeneratedNever();

                entity.Property(e => e.Reason1)
                    .HasMaxLength(50)
                    .HasColumnName("Reason");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Idrole)
                    .HasName("PK_Roluri");

                entity.Property(e => e.Idrole)
                    .ValueGeneratedNever()
                    .HasColumnName("IDRole");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasMany(d => d.Idpermissons)
                    .WithMany(p => p.Idroles)
                    .UsingEntity<Dictionary<string, object>>(
                        "PermissionRole",
                        l => l.HasOne<Permission>().WithMany().HasForeignKey("Idpermisson").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PermissionRoles_Permissions"),
                        r => r.HasOne<Role>().WithMany().HasForeignKey("Idrole").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PermissionRoles_Roles"),
                        j =>
                        {
                            j.HasKey("Idrole", "Idpermisson");

                            j.ToTable("PermissionRoles");

                            j.IndexerProperty<int>("Idrole").HasColumnName("IDRole");

                            j.IndexerProperty<short>("Idpermisson").HasColumnName("IDPermisson");
                        });
            });

            modelBuilder.Entity<Split>(entity =>
            {
                entity.HasKey(e => e.Idsplit);

                entity.ToTable("Split");

                entity.Property(e => e.Idsplit)
                    .ValueGeneratedNever()
                    .HasColumnName("IDSplit");

                entity.Property(e => e.Idcreator).HasColumnName("IDCreator");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.IdcreatorNavigation)
                    .WithMany(p => p.Splits)
                    .HasForeignKey(d => d.Idcreator)
                    .HasConstraintName("FK_Split_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser);

                entity.ToTable("User");

                entity.Property(e => e.Iduser)
                    .ValueGeneratedNever()
                    .HasColumnName("IDUser");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(1024);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasMany(d => d.Idroles)
                    .WithMany(p => p.Idusers)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("Idrole").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserRoles_Roles"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("Iduser").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserRoles_User"),
                        j =>
                        {
                            j.HasKey("Iduser", "Idrole");

                            j.ToTable("UserRoles");

                            j.IndexerProperty<Guid>("Iduser").HasColumnName("IDUser");

                            j.IndexerProperty<int>("Idrole").HasColumnName("IDRole");
                        });
            });

            modelBuilder.Entity<UserExercise>(entity =>
            {
                entity.HasKey(e => new { e.Iduser, e.Idworkout, e.Idexercise, e.Date });

                entity.ToTable("UserExercise");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Idworkout).HasColumnName("IDWorkout");

                entity.Property(e => e.Idexercise).HasColumnName("IDExercise");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EffortCoefficient).HasColumnType("numeric(18, 4)");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.UserExercises)
                    .HasForeignKey(d => new { d.Idworkout, d.Idexercise })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserExercise_Exercise");

                entity.HasOne(d => d.UserWorkout)
                    .WithMany(p => p.UserExercises)
                    .HasPrincipalKey(p => new { p.Iduser, p.Idworkout, p.Date })
                    .HasForeignKey(d => new { d.Iduser, d.Idworkout, d.Date })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserExercise_UserWorkout");
            });

            modelBuilder.Entity<UserExercisePr>(entity =>
            {
                entity.HasKey(e => new { e.Iduser, e.Idexercise, e.Idpr })
                    .HasName("PK_UserPrHistory");

                entity.ToTable("UserExercisePr");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Idexercise).HasColumnName("IDExercise");

                entity.Property(e => e.Idpr).HasColumnName("IDPr");

                entity.Property(e => e.OneRepMax).HasColumnType("numeric(18, 4)");

                entity.HasOne(d => d.IdexerciseNavigation)
                    .WithMany(p => p.UserExercisePrs)
                    .HasForeignKey(d => d.Idexercise)
                    .HasConstraintName("FK_UserPrHistory_Exercise");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.UserExercisePrs)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPrHistory_User");
            });

            modelBuilder.Entity<UserExerciseSet>(entity =>
            {
                entity.HasKey(e => new { e.Iduser, e.Idworkout, e.Idexercise, e.Date, e.Idset });

                entity.ToTable("UserExerciseSet");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Idworkout).HasColumnName("IDWorkout");

                entity.Property(e => e.Idexercise).HasColumnName("IDExercise");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Idset).HasColumnName("IDSet");

                entity.HasOne(d => d.UserExercise)
                    .WithMany(p => p.UserExerciseSets)
                    .HasForeignKey(d => new { d.Iduser, d.Idworkout, d.Idexercise, d.Date })
                    .HasConstraintName("FK_UserExerciseSet_UserExercise");
            });

            modelBuilder.Entity<UserPointsHistory>(entity =>
            {
                entity.HasKey(e => new { e.Iduser, e.ObtainDate });

                entity.ToTable("UserPointsHistory");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.ObtainDate).HasColumnType("datetime");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.UserPointsHistories)
                    .HasForeignKey(d => d.Iduser)
                    .HasConstraintName("FK_UserPointsHistory_User");

                entity.HasOne(d => d.Reason)
                    .WithMany(p => p.UserPointsHistories)
                    .HasForeignKey(d => d.Reasonid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserPointsHistory_Reason");
            });

            modelBuilder.Entity<UserSplit>(entity =>
            {
                entity.HasKey(e => new { e.Iduser, e.Idsplit });

                entity.ToTable("UserSplit");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Idsplit).HasColumnName("IDSplit");

                entity.HasOne(d => d.IdsplitNavigation)
                    .WithMany(p => p.UserSplits)
                    .HasForeignKey(d => d.Idsplit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSplit_Split");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.UserSplits)
                    .HasForeignKey(d => d.Iduser)
                    .HasConstraintName("FK_UserSplit_User");
            });

            modelBuilder.Entity<UserWeightHistory>(entity =>
            {
                entity.HasKey(e => new { e.Iduser, e.WeighingDate });

                entity.ToTable("UserWeightHistory");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.WeighingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Weighing_Date");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.UserWeightHistories)
                    .HasForeignKey(d => d.Iduser)
                    .HasConstraintName("FK_UserWeightHistory_User");
            });

            modelBuilder.Entity<UserWorkout>(entity =>
            {
                entity.HasKey(e => new { e.Iduser, e.Idsplit, e.Idworkout, e.Date });

                entity.ToTable("UserWorkout");

                entity.HasIndex(e => new { e.Iduser, e.Idworkout, e.Date }, "IX_UserWorkout")
                    .IsUnique();

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Idsplit).HasColumnName("IDSplit");

                entity.Property(e => e.Idworkout).HasColumnName("IDWorkout");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.WorkoutEffortCoefficient).HasColumnType("numeric(18, 4)");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.UserWorkouts)
                    .HasForeignKey(d => new { d.Iduser, d.Idsplit })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserWorkout_UserSplit");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.UserWorkouts)
                    .HasPrincipalKey(p => new { p.Idworkout, p.Idsplit })
                    .HasForeignKey(d => new { d.Idworkout, d.Idsplit })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserWorkout_Workout");
            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.HasKey(e => e.Idworkout);

                entity.ToTable("Workout");

                entity.HasIndex(e => new { e.Idworkout, e.Idsplit }, "IX_Workout")
                    .IsUnique();

                entity.Property(e => e.Idworkout)
                    .ValueGeneratedNever()
                    .HasColumnName("IDWorkout");

                entity.Property(e => e.Idsplit).HasColumnName("IDSplit");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.IdsplitNavigation)
                    .WithMany(p => p.Workouts)
                    .HasForeignKey(d => d.Idsplit)
                    .HasConstraintName("FK_Workout_Split");
            });

            modelBuilder.Entity<WorkoutExercise>(entity =>
            {
                entity.HasKey(e => new { e.Idworkout, e.Idexercise });

                entity.ToTable("WorkoutExercise");

                entity.Property(e => e.Idworkout).HasColumnName("IDWorkout");

                entity.Property(e => e.Idexercise).HasColumnName("IDExercise");

                entity.HasOne(d => d.IdexerciseNavigation)
                    .WithMany(p => p.WorkoutExercises)
                    .HasForeignKey(d => d.Idexercise)
                    .HasConstraintName("FK_WorkoutExercise_Exercise");

                entity.HasOne(d => d.IdworkoutNavigation)
                    .WithMany(p => p.WorkoutExercises)
                    .HasForeignKey(d => d.Idworkout)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkoutExercise_Workout");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
