

using Jandagashvili.speake.DLL.Kontext;

namespace Repositories
{
    public abstract class BaseRepository
    {
        public readonly Speakerdb database;
        protected BaseRepository()
        {
            this.database = new Speakerdb();
        }


    }
}
