

using Speaker.leison.Kontext;

namespace Repositories
{
    public abstract class BaseRepository
    {
        public readonly d database;
        protected BaseRepository(d db)
        {
            this.database = db;
        }


    }
}
