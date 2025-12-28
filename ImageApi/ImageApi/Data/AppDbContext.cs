using ImageApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
{
    public DbSet<Image> Images { get; set; }
}
