using System.IO;

namespace GeneratorLibrary
{
    public static class FolderManager
    {
        public static void CopyDir(DirectoryInfo dir, string destinationPath)
        {
            var files = dir.GetFiles();
            Directory.CreateDirectory(destinationPath);
            foreach (var file in files)
                File.Copy(file.FullName, string.Concat(destinationPath, "/", file.Name));
            var dirs = dir.GetDirectories();
            if (dirs.Length == 0) return;
            foreach (var directory in dirs)
                CopyDir(directory, destinationPath + "/" + directory.Name);
        }
    }
}