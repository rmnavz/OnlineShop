using Microsoft.EntityFrameworkCore;

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
            
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API


        }
    }
}