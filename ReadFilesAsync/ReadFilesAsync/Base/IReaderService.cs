using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFilesAsync.Base
{
    public interface IReaderService<T>
    {
        Task<T> ReadAsync(string path, CancellationToken cancellationToken);
    }
}
