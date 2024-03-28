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