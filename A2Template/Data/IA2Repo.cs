using A2.Models;

namespace A2.Data
{
    public interface IA2Repo
    {
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Organizor> GetAllOrganizors();
        IEnumerable<Product> GetAllProducts();
        IEnumerable<User> GetAllUsers();
        public User GetCustomerByUsername(string e);
        public Product GetProductById(int id);
        public Event GetEventById(int id);
        User AddUser(User user);
        public bool ValidUserLogin(string userName, string password);
        public bool ValidOrganizorLogin(string userName, string password);
        Event AddEvent(Event e);
        void SaveChanges();

    }
}