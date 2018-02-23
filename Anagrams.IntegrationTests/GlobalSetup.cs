using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using Anagrams.EFCF.Core;

namespace Anagrams.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        private ManagerContext context;

        [OneTimeSetUp]
        public void SetUp()
        {

            MigrateToLatestVersoin();
            Seed();
        }

     

        public void Seed()
        {
            context = new ManagerContext();

            if (context.Words.Any())
            {
                return;
            }

            context.Words.Add(new EFCF.Core.Models.Word
            {
                Name = "namas"
            }
            );
            context.Words.Add(new EFCF.Core.Models.Word
            {
                Name = "alus"
            }
           );
            context.Words.Add(new EFCF.Core.Models.Word
            {
                Name = "daug"
            }
           );

           context.SaveChanges();
        }

        public static void MigrateToLatestVersoin()
        {
            var configuration = new Anagrams
                  .EFCF.Core.Migrations.Configuration();

            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
