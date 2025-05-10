using Microsoft.EntityFrameworkCore;

namespace BeansAPI.Models;

public class BeanContext : DbContext
{
    public DbSet<Bean> Beans { get; set; } = null!;
    public DbSet<BeanOfTheDay> BeanOfTheDay { get; set; } = null!;
    public string DbPath { get; }
    
    public BeanContext(DbContextOptions<BeanContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "AllTheBeans.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite($"Data Source={DbPath}")
            .UseSeeding((_,_) =>
            {
                // Seed beans table TODO - from JSON file
                var testBean = Beans.FirstOrDefault(b => b.Name == "Test Bean");
                if (testBean == null)
                {
                    Beans.Add(new Bean
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test Bean",
                        Description = "This is a test bean.",
                        ImageUrl = "https://example.com/testbean.jpg",
                        CostGBP = 1.99f,
                        Colour = "Brown",
                        Country = "Testland",
                    });
                    this.SaveChanges();
                }

                // Seed bean of the day table
                BeanOfTheDay? testBoTd = BeanOfTheDay.FirstOrDefault();
                int BoTdTableSize = BeanOfTheDay.Count();
                if (testBoTd == null || BoTdTableSize > 1)
                {
                    BeanOfTheDay.RemoveRange(BeanOfTheDay);                    
                    BeanOfTheDay.Add(new BeanOfTheDay
                    {
                        Id = 0,
                        BeanId = Guid.NewGuid(),
                    });
                    this.SaveChanges();
                    chooseBeanOfTheDay(); 
                }
            });
    }

    private void chooseBeanOfTheDay()
    {
        Guid currentBeanOfTheDay = BeanOfTheDay.FirstOrDefault()!.BeanId;
        Guid RandomGuid = Guid.NewGuid(); // Needs to be generated before Linq query is executed.
        Guid chosenBean = Beans.Where(b => b.Id != currentBeanOfTheDay)
                            .OrderBy(b => RandomGuid) // Random order
                            .Select(b => b.Id).FirstOrDefault();
        BeanOfTheDay.FirstOrDefault()!.BeanId = chosenBean;
        this.SaveChanges();
    }
}