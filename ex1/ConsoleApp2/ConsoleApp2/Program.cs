using System;
using System.IO;

class MainClass
{
    public static DateTime najstarsza = DateTime.MaxValue;
    public static void Main(string[] args)
    {
        var dir = args[0];
        PrintDirectoryTree(dir, 16, new string[] { "folder3" });
        Console.WriteLine(najstarsza);
       // Console.WriteLine(oldest(args[0]));
    }


    public static void PrintDirectoryTree(string directory, int lvl, string[] excludedFolders = null, string lvlSeperator = "")
    {

        excludedFolders = excludedFolders ?? new string[0];

        foreach (string f in Directory.GetFiles(directory))
        {
            FileSystemInfo fileInfo = new DirectoryInfo(directory).GetFileSystemInfos()
           .OrderBy(fi => fi.CreationTime).First();

            if (najstarsza > fileInfo.CreationTime)
                najstarsza = fileInfo.CreationTime;
            long length = new System.IO.FileInfo(f).Length;

            Console.Write(lvlSeperator + " " + length.ToString() + "bajtow " +  Path.GetFileName(f));
            WriteDos(f);
        }

        foreach (string d in Directory.GetDirectories(directory))
        {

            //int fileCount = Directory.GetFiles(d).Length;
            int fileCount = Directory.GetFiles(d, "*.*", SearchOption.AllDirectories).Length;
            Console.Write(lvlSeperator + "-" + Path.GetFileName(d) + " " + fileCount.ToString() + "plikow");
            WriteDos(d);
            if (lvl > 0)
            {

                PrintDirectoryTree(d, lvl - 1, excludedFolders, lvlSeperator + "  ");
            }
        }
    }


    public static string oldest(string path)
    {
        FileSystemInfo fileInfo = new DirectoryInfo(path).GetFileSystemInfos()
        .OrderBy(fi => fi.CreationTime).First();

        return fileInfo.LastWriteTime.ToString();
    }

    public static void WriteDos(string filePath)
    {
        Console.Write(" ");
        bool isReadOnly = ((File.GetAttributes(filePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
        if (isReadOnly)
        {
            Console.Write("R");
        }
        else
        {
            Console.Write("-");
        }
        // check whether a file is hidden
        bool isHidden = ((File.GetAttributes(filePath) & FileAttributes.Hidden) == FileAttributes.Hidden);

        if (isHidden)
        {
            Console.Write("H");
        }
        else
        {
            Console.Write("-");
        }
        // check whether a file has archive attribute
        bool isArchive = ((File.GetAttributes(filePath) & FileAttributes.Archive) == FileAttributes.Archive);

        if (isArchive)
        {
            Console.Write("A");
        }
        else
        {
            Console.Write("-");
        }

        // check whether a file is system file
        bool isSystem = ((File.GetAttributes(filePath) & FileAttributes.System) == FileAttributes.System);

        if (isSystem)
        {
            Console.WriteLine("S");
        }
        else
        {
            Console.WriteLine("-");
        }
    }
}