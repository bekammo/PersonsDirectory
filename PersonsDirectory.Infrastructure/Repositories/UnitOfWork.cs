using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Infrastructure.Persistence;

namespace PersonsDirectory.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonsDirectoryDbContext _context;

        public UnitOfWork(PersonsDirectoryDbContext context)
        {
            _context = context;
            Persons = new PersonRepository(_context);
        }

        public IPersonRepository Persons { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
