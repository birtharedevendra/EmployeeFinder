//using Eda.RDBI.Models.DataObjects;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Silicus.FrameWorx.Utility;
using Silicus.Finder.Entities.EntityConfigurations;
using Silicus.Finder.Models.DataObjects;

namespace Silicus.Finder.Entities
{
    /// <summary>
    /// This class registers the entities that are comprised
    /// in the model.
    /// </summary>
    public abstract class DataContextBase : DbContext
    {
        /// <summary>
        /// Calls the base class with the given connection string.
        /// </summary>
        protected DataContextBase(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //  Disable the default PluralizingTableNameConvention 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 

            // Register Entities.
            Guard.ArgumentNotNull(modelBuilder, "modelBuilder");

            modelBuilder.Configurations.Add(new OrganizationMap());

            modelBuilder.Configurations.Add(new ProjectMap());

            modelBuilder.Configurations.Add(new ProjectDetailMap());

            modelBuilder.Configurations.Add(new ManagerDetailMap());

            modelBuilder.Configurations.Add(new EmailAvailableMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RolesMap());

            // Many-to-many example - can be moved to Map file as well.
            modelBuilder.Entity<Asset>()
            .HasMany<Category>(s => s.Categories)
            .WithMany(c => c.Assets)
            .Map(cs =>
            {
                cs.MapLeftKey("AssetId");
                cs.MapRightKey("CategoryId");
                cs.ToTable("AssetCategory");
            });
        }
    }
}