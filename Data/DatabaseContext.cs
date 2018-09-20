using Microsoft.EntityFrameworkCore;
using Online_Shop.Models;

namespace Online_Shop
{
    /// <summary>
    /// The database representational model of the Application
    /// </summary>
    public class DatabaseContext : DbContext
    {
        #region Constructor

        /// <summary>
        /// Default constructor, expecting database options passed in
        /// </summary>
        /// <param name="options">The database context options</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #endregion

        #region Public Properties
            public DbSet<AccountModel> Accounts { get; set; }
            public DbSet<SellerModel> Sellers { get; set; }
            public DbSet<CustomerModel> Customers { get; set; }
            
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Fluent API

            
                
            #endregion

        }
    }
}