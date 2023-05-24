namespace TakeAway.Model
{
    public class Order
    {
        public int Id { get; set; }      

        public int Price { get; set; }

        public List<Item>? Items { get; set; }

        public Customer? OrderCustomer { get; set; }


    }
}
