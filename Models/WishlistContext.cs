using Microsoft.EntityFrameworkCore;

namespace WishlistApi.Models
{
    public class WishlistContext : DbContext 
    {
        public WishlistContext(DbContextOptions<WishlistContext> options) : base (options)
        {

        }
        public DbSet<WishlistItem> WishlistItems { get; set; }
    }
    
}