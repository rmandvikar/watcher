using System.IO;
using System.Security.Permissions;
using System.Threading;

namespace rm.Watcher
{
    public class Watcher
    {
        string source;
        string target;

        public Watcher(string source, string target)
        {
            this.source = source;
            this.target = target;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run()
        {
            var watcher = new FileSystemWatcher();
            watcher.Path = source;
            watcher.NotifyFilter =
                NotifyFilters.LastWrite
                | NotifyFilters.FileName
                | NotifyFilters.DirectoryName
                //| NotifyFilters.LastAccess // not needed
                ;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.Deleted += new FileSystemEventHandler(OnDeleted);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.IncludeSubdirectories = true;

            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (IsDir(e.FullPath)) // dir
            {
                // dir
                Directory.CreateDirectory(Path.Combine(target, e.Name));
            }
            else // file
            {
                // keep trying till open access to file
                while (true)
                {
                    try
                    {
                        using (File.Open(e.FullPath, FileMode.Open, FileAccess.Read))
                        { }
                        break;
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(100);
                    }
                }
                // create dir if not
                var dir = Path.GetDirectoryName(e.FullPath.Replace(this.source, this.target));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                // copy file
                File.Copy(e.FullPath, Path.Combine(target, e.Name), true);
            }
        }
        private bool IsDir(string path)
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        }
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            // do nothing
        }
        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            // do nothing
        }
        private void OnRenamed(object source, RenamedEventArgs e)
        {
            if (IsDir(Path.Combine(target, e.OldName)))
            {
                Directory.Move(Path.Combine(target, e.OldName), Path.Combine(target, e.Name));
            }
            else
            {
                File.Move(Path.Combine(target, e.OldName), Path.Combine(target, e.Name));
            }
        }
    }
}
