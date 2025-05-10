using System.Text.Json;
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
                // If we have beans and exactly one bean of the day entry, 
                // we can assume the database has been seeded and probably isn't in an error state
                if (Beans.Any() && BeanOfTheDay.Count() == 1) return;

                // Reset the database in case there are any invalid entries
                Beans.RemoveRange(Beans);
                BeanOfTheDay.RemoveRange(BeanOfTheDay);

                string SeededBeanOfTheDay = "";

                BeanDTO[] seedBeans = JsonSerializer.Deserialize<BeanDTO[]>(File.ReadAllText("Data/SeedBeanData.json")) ?? Array.Empty<BeanDTO>();
                foreach (BeanDTO seedBean in seedBeans)
                {
                    Beans.Add(seedBean.toBean());
                    if (seedBean.IsBOTD)
                    {
                        // If the seed data has multiple beans of the day, we will just take the last one
                        SeededBeanOfTheDay = seedBean._id;
                    }
                }

                BeanOfTheDay.Add(new BeanOfTheDay
                {
                    Id = 0,
                    BeanId = SeededBeanOfTheDay.ToString(),
                    DateSet = DateTime.UnixEpoch,
                });

                this.SaveChanges();
            });
    }

    public void ChooseBeanOfTheDay()
    {
        // Do nothing if we've set the bean of the day in the last 23 hours (to allow for slight time drift on the daily hangfire task)
        DateTime timeLastSet = BeanOfTheDay.First().DateSet;
        if (DateTime.Now <= timeLastSet.AddDays(0.96)) return;

        string currentBeanOfTheDay = BeanOfTheDay.First().BeanId;
        string chosenBean = Beans.Where(b => b.Id != currentBeanOfTheDay)
                            .AsEnumerable() // AsEnumerable to use LINQ to Objects for random ordering
                            .OrderBy(b => Guid.NewGuid()) // Random order
                            .Select(b => b.Id).First();
        BeanOfTheDay.First().BeanId = chosenBean;
        BeanOfTheDay.First().DateSet = DateTime.Now;
        this.SaveChanges();
    }
}