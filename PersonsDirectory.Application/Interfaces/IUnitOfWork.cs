using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository Persons { get; }
        Task<int> CompleteAsync();
        void Dispose();
    }
}
