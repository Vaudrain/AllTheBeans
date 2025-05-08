using Microsoft.EntityFrameworkCore;

namespace BeansAPI.Models;

public class BeanContext : DbContext
{
    public BeanContext(DbContextOptions<BeanContext> options)
        : base(options)
    {
    }

    public DbSet<Bean> Beans { get; set; } = null!;
}