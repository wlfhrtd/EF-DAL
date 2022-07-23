using System;
using Model.Entities;
using Model.Entities.Owned;
using Dal.Exceptions;
using Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Dal.EfStructures
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            SavingChanges += (sender, args)
                => Console.WriteLine($"Saving changes for {((ApplicationDbContext)sender)!.Database!.GetConnectionString()}");

            SavedChanges += (sender, args)
                => Console.WriteLine(
                    $"Saved {args!.EntitiesSavedCount} changes" +
                    $" for {((ApplicationDbContext)sender)!.Database!.GetConnectionString()}");

            SaveChangesFailed += (sender, args)
                => Console.WriteLine($"An exception occurred! {args.Exception.Message} entities");

            ChangeTracker.Tracked += ChangeTracker_Tracked;
            ChangeTracker.StateChanged += ChangeTracker_StateChanged;
        }


        public DbSet<CreditRisk>? CreditRisks { get; set; }

        public DbSet<Customer>? Customers { get; set; }

        public DbSet<Car>? Cars { get; set; }

        public DbSet<Make>? Makes { get; set; }

        public DbSet<Order>? Orders { get; set; }

        public virtual DbSet<CustomerOrderViewModel>? CustomerOrderViewModels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CreditRisk>(entity =>
            {
                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p!.CreditRisks)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CreditRisks_Customers");

                entity.OwnsOne(o => o.PersonalInformation,
                    pd =>
                    {
                        pd.Property<string>(nameof(Person.FirstName))
                        .HasColumnName(nameof(Person.FirstName))
                        .HasColumnType("nvarchar(50)");
                        pd.Property<string>(nameof(Person.LastName))
                        .HasColumnName(nameof(Person.LastName))
                        .HasColumnType("nvarchar(50)");
                        pd.Property(p => p.FullName)
                        .HasColumnName(nameof(Person.FullName))
                        .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
                    });
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.OwnsOne(o => o.PersonalInformation,
                    pd =>
                    {
                        pd.Property(p => p.FirstName).HasColumnName(nameof(Person.FirstName));
                        pd.Property(p => p.LastName).HasColumnName(nameof(Person.LastName));
                        pd.Property(p => p.FullName)
                        .HasColumnName(nameof(Person.FullName))
                        .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
                    });

            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(p => p.IsDrivable)
                .HasField("_isDrivable")
                .HasDefaultValue(true);

                entity.HasOne(d => d.MakeNavigation)
                    .WithMany(p => p!.Cars)
                    .HasForeignKey(d => d.MakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Make_Inventory");

                entity.HasQueryFilter(c => c.IsDrivable);
            });

            modelBuilder.Entity<Make>(entity =>
            {
                entity.HasMany(e => e.Cars)
                .WithOne(c => c.MakeNavigation!)
                .HasForeignKey(c => c.MakeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Make_Inventory");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.CarNavigation)
                    .WithMany(p => p!.Orders)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Inventory");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p!.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Orders_Customers");

                modelBuilder.Entity<Order>().HasQueryFilter(e => e.CarNavigation!.IsDrivable);
            });

            modelBuilder.Entity<CustomerOrderViewModel>(entity =>
            {
                entity.HasNoKey().ToView("CustomerOrderView", "dbo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new CustomConcurrencyException("Concurrency exception occurred", e);
            }
            catch (RetryLimitExceededException e)
            {
                // DbResiliency retry limit exceeded
                throw new CustomRetryLimitExceededException("RetryLimitExceeded exception occurred; check db server", e);
            }
            catch (Exception e)
            {
                throw new CustomException("An error occurred while updating the database", e);
            }
        }

        private void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs args)
        {
            var source = (args.FromQuery) ? "Database" : "Code";
            if (args.Entry.Entity is Car c)
            {
                Console.WriteLine($"Car entry {c.PetName} was added from {source}");
            }
        }

        private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs args)
        {
            if (args.Entry.Entity is not Car c)
            {
                return;
            }

            Console.WriteLine($"Car {c.PetName} had been {args.OldState} until state changed to {args.NewState}");

            var action = string.Empty;
            switch (args.NewState)
            {
                case EntityState.Unchanged:
                    action = args.OldState switch
                    {
                        EntityState.Added => "Added",
                        EntityState.Modified => "Edited",
                        _ => action,
                    };
                    Console.WriteLine($"The object was {action}");
                    break;
            }
        }
    }
}
