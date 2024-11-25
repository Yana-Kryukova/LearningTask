using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ReadFilesAsync.Base;

namespace ReadFilesAsync
{
    public class ReaderAllTextService : IReaderService<int>
    {
        public async Task<int> ReadAsync(string path, CancellationToken cancellationToken)
        {
            if (!File.Exists(path))
                return -1;

            var res = await File.ReadAllTextAsync(path, cancellationToken);
            return CalculateCharacterInString(res);
        }

        private int CalculateCharacterInString(string str, char character = ' ')
        => str.Count(x => x == character);
    }
}
