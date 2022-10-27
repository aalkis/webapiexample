using WebApi.Interfaces;

namespace WebApi.Repositories
{
    public class DummyRepo : IDummyRepo
    {
        public string GetName()
        {
            return "Yavuz";
        }
    }
}
