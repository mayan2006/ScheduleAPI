using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Core.Repositories
{
    public interface IRepositoryManager
    {
        IStudentRepository Students { get; }
        void Save();
    }
}
