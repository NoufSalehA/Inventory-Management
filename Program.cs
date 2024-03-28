using System.Security.Cryptography.X509Certificates;

class Program
{
    public class Item
    {

        public string Name
        {//readonly
            get;//get the data
            set;
        }
        private int _quantity;//i add it because it requires to create private quantity
        public int Quantity//to use it in sum method it must be public
        {
            get;
            set;
        }
        //DateTime is a data type
        public DateTime CreatedDate
        {
            get;
            private set;
        }
        public Item(string name, int quantity, DateTime createdDate = default)//helps to assign the value later
        {
            if (quantity < 0)
            {
                throw new ArgumentException("---The quantity can not be negative---");
            }
            Name = name;
            Quantity = quantity;
            CreatedDate = createdDate == default ? DateTime.Now : createdDate;//time now

        }
        //method ToString() used to convert to string 
        //return a string represent current object
        public override string ToString()//property so the letter is capital
        {
            return $"Item Name: {Name} , Quantity of Items: {Quantity} , Created Date : {CreatedDate}";
        }
    }
    class Store
    {
        private List<Item> items = new List<Item>();//empty collection
                                                    //class name Item
        private int maxCapacity;//capacity of items
        public Store(int maxCapacityConst)
        {
            this.maxCapacity = maxCapacityConst;

        }//<=constructor for capacity

        public void AddItem(Item item)
        {//Modify the add method to not overload the capacity
            if (GetCurrentVolume() + item.Quantity > maxCapacity)
            {
                Console.WriteLine("===CAN NOT ADD !===The limit of capacity is 100 items");

            }
            //do not add items if it is already there
            bool isItemExist = items.Any((product) => product.Name == item.Name);//inside any a condition if they have the same name than do not add it or delete it
            if (isItemExist)
            {
                Console.WriteLine($"Item is already added");
            }//items the list
            else
            {
                items.Add(item);
            }
        }
        public void PrintList()
        {
            foreach (var i in items)
            {
                Console.WriteLine($"{i}");
            }

        }
        public void DeleteItem(string itemName)
        {// delete items based on name
            Item? itemDelete = items.FirstOrDefault((product) => product.Name == itemName);//item because it will return an item
            if (itemDelete != null)
            {

                items.Remove(itemDelete);
                Console.WriteLine($"----Deleted----");
            }
            else
            {
                Console.WriteLine("---Item is not found---");

            }
        }
        public int GetCurrentVolume()
        {//compute the total of quantities

            return items.Sum(quantities => quantities.Quantity);
        }
        public Item? FindItemByName(string itemName)//return an item
        {//Method FindItemByName to find an item by its name.
            Item? findsByName = items.FirstOrDefault((theName) => theName.Name == itemName);

            return findsByName;
        }
        public List<Item> SortByNameAsc()//return a list
        {
            return items.OrderBy(product => product.Name).ToList();//convert it to list

        }
        public IEnumerable<Item> SortByDate(SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.ASC)
            {
                return items.OrderBy(i => i.CreatedDate);
            }
            else
            {
                return items.OrderByDescending(i => i.CreatedDate);
            }
        }
        public enum SortOrder
        {
            ASC,
            DESC
        }
        public (IEnumerable<Item> NewArrivals, IEnumerable<Item> OldItems) GroupByDate()
        {
            var currentDate = DateTime.Now;
            var threeMonthsAgo = currentDate.AddMonths(-3);
            var newArrivals = items.Where(item => item.CreatedDate >= threeMonthsAgo);
            var oldItems = items.Where(item => item.CreatedDate < threeMonthsAgo);
            return (newArrivals, oldItems);
        }
    }

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