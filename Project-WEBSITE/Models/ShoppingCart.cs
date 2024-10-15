namespace Project_WEBSITE.Models
{
    public class ShoppingCart
    {

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        // Thuộc tính để truy cập tổng số lượng các mục trong giỏ hàng

        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {

                Items.Add(item);
            }

            // Cập nhật tổng số lượng sau khi thêm một mục mới vào giỏ hàng
        }

        public void RemoveItem(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                // Trừ đi số lượng của mục đã xóa khỏi tổng số lượng
                Items.Remove(item);
            }
        }
    }
}
