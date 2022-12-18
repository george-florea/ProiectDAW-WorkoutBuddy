using Backend.Common;
using Backend.Entities;
using Backend.Entities;

namespace Backend.DataAccess
{
    public class UnitOfWork
    {
        private readonly WorkoutBuddyDBContext Context;

        public UnitOfWork(WorkoutBuddyDBContext context)
        {
            Context = context;
        }

        /*private IRepository<Person> persons;
        public IRepository<Person> Persons => persons ?? (persons = new BaseRepository<Person>(Context));

        private IRepository<City> cities;
        public IRepository<City> Cities => cities ?? (cities = new BaseRepository<City>(Context));*/

        private IRepository<User> users;
        public IRepository<User> Users => users ?? (users = new BaseRepository<User>(Context));

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
