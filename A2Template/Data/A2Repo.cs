using A2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace A2.Data
{
    public class A2Repo : IA2Repo
    {
        private readonly A2DbContext _dbContext;
        public A2Repo(A2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Event AddEvent(Event e)
        {
            EntityEntry<Event> p = _dbContext.Events.Add(e);
            Event c = p.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public User AddUser(User user)
        {
            EntityEntry<User> e = _dbContext.Users.Add(user);
            User c = e.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            IEnumerable<Event> events = _dbContext.Events.ToList<Event>();
            return events;
        }

        public IEnumerable<Organizor> GetAllOrganizors()
        {
            IEnumerable<Organizor> organizors = _dbContext.Organizors.ToList<Organizor>();
            return organizors;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> products = _dbContext.Products.ToList<Product>();
            return products;
        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = _dbContext.Users.ToList<User>();
            return users;
        }

        public User GetCustomerByUsername(string e)
        {
            User user = _dbContext.Users.FirstOrDefault(o => o.Username == e);
            return user;
        }

        public Product GetProductById(int id)
        {
            Product product = _dbContext.Products.FirstOrDefault(o => o.Id == id);
            return product;
        }

        public Event GetEventById(int id)
        {
            Event e = _dbContext.Events.FirstOrDefault(o => o.Id == id);
            return e;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public bool ValidUserLogin(string userName, string password)
        {
            User u = _dbContext.Users.FirstOrDefault(e => e.Username == userName && e.Password == password);
            if (u == null)
                return false;
            else
                return true;
        }
        public bool ValidOrganizorLogin(string userName, string password)
        {
            Organizor o = _dbContext.Organizors.FirstOrDefault(e => e.Name == userName && e.Password == password);
            if (o == null)
                return false;
            else
                return true;
        }
    }
}