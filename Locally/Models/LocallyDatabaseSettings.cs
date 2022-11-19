using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locally.Models
{
    public class LocallyDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string TeamsCollectionName { get; set; } = null!;

        public string GamesCollectionName { get; set; } = null!;

    }
}