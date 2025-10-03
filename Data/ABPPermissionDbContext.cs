using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Gdpr;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using ABPPermission.Entities.Books;
using ABPPermission.Entities.Authors;

namespace ABPPermission.Data;

public class ABPPermissionDbContext : AbpDbContext<ABPPermissionDbContext>
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    
    public const string DbTablePrefix = "App";
    public const string DbSchema = null;

    public ABPPermissionDbContext(DbContextOptions<ABPPermissionDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigurePermissionManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureGdpr();
        builder.ConfigureLanguageManagement();
        builder.ConfigureTextTemplateManagement();
        
        builder.Entity<Book>(b =>
        {
            b.ToTable(DbTablePrefix + "Books",
                DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            
            // Configure relationship with Author
            b.HasOne(x => x.Author)
             .WithMany(x => x.Books)
             .HasForeignKey(x => x.AuthorId)
             .IsRequired();
        });

        builder.Entity<Author>(b =>
        {
            b.ToTable(DbTablePrefix + "Authors",
                DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.FullName).IsRequired().HasMaxLength(256);
            b.Property(x => x.PhoneNumber).HasMaxLength(15);
            b.Property(x => x.Address).HasMaxLength(500);
            b.Property(x => x.Biography).HasMaxLength(2000);
        });
        
        /* Configure your own entities here */
    }
}

