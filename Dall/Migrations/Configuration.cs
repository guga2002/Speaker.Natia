namespace Speaker.leison.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Speaker.leison.Kontext.d>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Speaker.leison.Kontext.SpeakerDb";
        }

        protected override void Seed(Speaker.leison.Kontext.d context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
