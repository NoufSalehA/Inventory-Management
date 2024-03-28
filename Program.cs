using System.Security.Cryptography.X509Certificates;

class Program
{
    public static void Main(string[] args)
    {
        var chocolateBar = new Item("Chocolate Bar", 15, new DateTime(2023, 2, 1));
        var coffee = new Item("Coffee", 20);//output is nothing until i create override method
        var coffee1 = new Item("Coffee", 20);
        var chipsBag = new Item("Chips Bag", 26, new DateTime(2023, 6, 1));
        var sodaCan = new Item("Soda Can", 8, new DateTime(2023, 7, 1));
        var sandwich = new Item("Sandwich", 15);//deleted

        //access the list
        var store = new Store(100);//capacity added "100"
        store.AddItem(coffee);//adding item
        store.AddItem(chocolateBar);
        store.AddItem(coffee1);//item is already added
        store.AddItem(chipsBag);
        store.AddItem(sodaCan);
        store.AddItem(sandwich);

        Console.WriteLine($"Before sorting by date:");
        store.PrintList();//print the list
        Console.WriteLine($"\n****************************************************************************************");

        store.DeleteItem("Sandwich");//deleting item by its name
        store.PrintList();
        Console.WriteLine($"\n****************************************************************************************");
        Console.WriteLine($"The Quantity of items is :{store.GetCurrentVolume()}");
        Console.WriteLine("\n***************************************************************************************\n");
        Console.WriteLine($"Find item by name :\n{store.FindItemByName("Chocolate Bar")}");
        Console.WriteLine("\n***************************************************************************************\n \n");
        var sortedItems = store.SortByNameAsc();
        Console.WriteLine("-----Sorted List----\n");
        foreach (var sorted in sortedItems)
        {
            Console.WriteLine($"{sorted}");
        }
        Console.WriteLine($"\n *************************************************************************************");
        Console.WriteLine($"ASC");

        var collectionSortedByDateAsc = store.SortByDate(Store.SortOrder.ASC);
        Console.WriteLine($"After sorting the date:");
        foreach (var d in collectionSortedByDateAsc)
        {
            Console.WriteLine($"{d}");

        }
        Console.WriteLine($"\n *************************************************************************************");
        Console.WriteLine($"DESC");

        var collectionSortedByDateDesc = store.SortByDate(Store.SortOrder.DESC);
        Console.WriteLine($"After sorting the date:");
        foreach (var d in collectionSortedByDateDesc)
        {
            Console.WriteLine($"{d}");

        }

        var groupByDate = store.GroupByDate();
        Console.WriteLine("*******************************************************************");

        Console.WriteLine("New Arrival Items:");
        foreach (var item in groupByDate.NewArrivals)
        {
            Console.WriteLine($" [New] - {item.Name}, Created: {item.CreatedDate.ToShortDateString()}");
        }
        Console.WriteLine($"*******************************************************************");

        Console.WriteLine("\nOld Items:");
        int number = 1;

        foreach (var item in groupByDate.OldItems)
        {
            Console.WriteLine($" {number} - {item.Name}, Created: {item.CreatedDate.ToShortDateString()}");
            number++;

        }
    }

}