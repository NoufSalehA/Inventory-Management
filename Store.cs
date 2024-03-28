using System.Security.Cryptography.X509Certificates;

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