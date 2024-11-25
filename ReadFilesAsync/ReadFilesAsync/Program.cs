using ReadFilesAsync.Base;
using System.Diagnostics;
using System.Reflection;

namespace ReadFilesAsync
{
    internal class Program(IReaderService<int> service)
    {
        public async Task GetCountCharacterInFiles1(string directory, CancellationToken cancellationToken)
        {
            // Only get files that are text files only as you want only .txt 
            string[] dirs = Directory.GetFiles(directory, "*.txt");
            foreach (var file in dirs)
            {
                var res = await service.ReadAsync(file, cancellationToken);
                Console.WriteLine($"In file {file} is {res} spaces");
            }
        }
        public async Task GetCountCharacterInFiles2(string directory, CancellationToken cancellationToken)
        {
            // Only get files that are text files only as you want only .txt 
            string[] dirs = Directory.GetFiles(directory, "*.txt");
            var list = new List<Task<int>>();
            foreach (var file in dirs)
            {
                list.Add(service.ReadAsync(file, cancellationToken));
            }
            Task.WaitAll(list.ToArray());
            int i = 0;
            foreach (var res in list)
            {
                Console.WriteLine($"In file {i+1} is {res.Result} spaces");
            }

        }
        static async Task Main(string[] args)
        {
            var p = new Program(new ReaderAllTextService());
            var directory = Path.GetFullPath("../../../TestDirectory");

            var watch = new Stopwatch();
            watch.Start();
            await p.GetCountCharacterInFiles1(directory, CancellationToken.None);
            watch.Stop();
            WriteWatch(watch);
            watch.Start();
            await p.GetCountCharacterInFiles2(directory, CancellationToken.None);
            watch.Stop();
            WriteWatch(watch);
        }
        private static void WriteWatch(Stopwatch w)
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = w.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
