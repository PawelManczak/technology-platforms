using System;
using System.Globalization;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;

class MainClass
{
    public static DateTime najstarsza = DateTime.MaxValue;
    public static List<(string name, long size)> kolekcja = new List<(string name, long size)>();

    public static void Main(string[] args)
    {
        var dir = args[0];
        PrintDirectoryTree(dir, 16, new string[] { "folder3" });
        Console.WriteLine(najstarsza);
        loadOnlyMainFolderFiles(args[0]);
        kolekcja.Sort(new listComparer());
        foreach (var item in kolekcja) {
            Console.WriteLine(item.name + " -> " + item.size);
        }

        FileStream fs = new FileStream("DataFile.dat", FileMode.Create);

        // Construct a BinaryFormatter and use it to serialize the data to the stream.
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, kolekcja);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
        Deserialize();
    }

    static void Deserialize()
    {
        // Declare the hashtable reference.
           List<(string name, long size)> kolekcja1 = new List<(string name, long size)>();

    // Open the file containing the data that you want to deserialize.
    FileStream fs = new FileStream("DataFile.dat", FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            // Deserialize the hashtable from the file and
            // assign the reference to the local variable.
            kolekcja1 = (List<(string name, long size)>)formatter.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }

        // To prove that the table deserialized correctly,
        // display the key/value pairs.
        foreach ((string name, long size) de in kolekcja1)
        {
            Console.WriteLine("{0} ->{1}.", de.name, de.size);
        }
    }
    public class listComparer : IComparer<(string, long)>
    {
        public int Compare((string, long) x, (string, long) y)
        {
            if(x.Item1.Length >  y.Item1.Length) return -1;
            if (x.Item1.Length < y.Item1.Length) return 1;
            return String.Compare(x.Item1, y.Item1);
        }
    }

    public static void loadOnlyMainFolderFiles(string directory)
    {
        foreach (string f in Directory.GetFiles(directory))
        {
            long length = new System.IO.FileInfo(f).Length;

            kolekcja.Add((Path.GetFileName(f), length));
   
        }

        foreach (string d in Directory.GetDirectories(directory))
        {
            int fileCount = Directory.GetFiles(d, "*.*", SearchOption.AllDirectories).Length;
            kolekcja.Add((Path.GetFileName(d), fileCount));
        }
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