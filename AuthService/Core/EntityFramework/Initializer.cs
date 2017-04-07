using System.Data.Entity;

namespace AuthService.Core.EntityFramework
{
    /// <summary>
    /// Tells Entity Framework to automatically upgrade the database based on the Context and Configuration
    /// </summary>
    public class Initializer : MigrateDatabaseToLatestVersion<UsersContext, Configuration>
    {

    }
}