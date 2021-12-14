using System.Threading.Tasks;


namespace GlobalCollege.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();

        Task<bool> CommitAsync();

    }
}
