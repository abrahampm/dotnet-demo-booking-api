using alten_test.DataAccessLayer.Context;

namespace alten_test.DataAccessLayer.Interfaces
{
    public interface IDatabaseContextFactory
    {
        DatabaseContext GetContext();
    }
}