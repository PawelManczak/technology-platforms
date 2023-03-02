using System.Collections.Generic;

namespace WpfApp1.Model
{
    public class Item
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }


    public class FileItem : Item
    {

    }

    public class DirectoryItem : Item
    {
        public List<Item> Items { get; set; }

        public DirectoryItem()
        {
            Items = new List<Item>();
        }
    }
}
