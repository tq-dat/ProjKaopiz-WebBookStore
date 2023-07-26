namespace WebBookStore.Dto
{
    public class OrderCreate
    {
        public OrderDto orderDto { get; set; }
        public List<int> cartItemIds { get; set; }
    }
}
