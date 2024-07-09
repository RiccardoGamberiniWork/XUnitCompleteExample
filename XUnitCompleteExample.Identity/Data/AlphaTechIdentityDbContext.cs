namespace XUnitCompleteExample.Identity.CommonLib.Data;

public class XUnitCompleteExampleIdentityDbContext : DbContext
{
    public XUnitCompleteExampleIdentityDbContext(DbContextOptions<XUnitCompleteExampleIdentityDbContext> options): base(options){}

    public DbSet<User> Users { get; set; }
}