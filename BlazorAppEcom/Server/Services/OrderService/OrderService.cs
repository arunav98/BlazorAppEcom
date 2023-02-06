using BlazorAppEcom.Server.Data;
using System.Security.Claims;

namespace BlazorAppEcom.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IAddressService _addressService;

        public OrderService(DataContext context, ICartService cartService,
            IAuthService authService,IAddressService addressService)
        {
            _context = context;
            _cartService = cartService;
            _authService = authService;
            _addressService = addressService;
        }

        public async Task<ServiceResponse<OrderDetailsResponseDTO>> GetOrderDetails(int orderId)
        {
            var response = new ServiceResponse<OrderDetailsResponseDTO>();
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductType)
                .Where(o => o.UserId == _authService.GetUserId() && o.Id == orderId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();
            if(order == null)
            {
                response.Success=false;
                response.Message = "order Not Found";
                return response;
            }

            var orderDetailsResponse = new OrderDetailsResponseDTO
            {
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Products = new List<OrderDetailsProductResponseDTO>(),
                Name = order.Name,
                Street = order.Street,
                City= order.City,
                State= order.State,
                Pincode= order.Pincode,
                Country= order.Country,
            };

            order.OrderItems.ForEach(item =>
            orderDetailsResponse.Products.Add(new OrderDetailsProductResponseDTO
            {
                ProductId = item.productId,
                ImageUrl = item.Product.ImageUrl,
                ProductType = item.ProductType.Name,
                Quantity = item.Quantity,
                Title = item.Product.Title,
                TotalPrice = item.TotalPrice
            }));

            response.Data = orderDetailsResponse;
            return response;
        }

        public async Task<ServiceResponse<List<OrderOverviewResponseDTO>>> GetOrderOverview()
        {
            var response = new ServiceResponse<List<OrderOverviewResponseDTO>>();
            var orders = await _context.Orders
                .Include(o=>o.OrderItems)
                .ThenInclude(oi=>oi.Product)
                .Where(o=>o.UserId == _authService.GetUserId())
                .OrderByDescending(o=>o.OrderDate)
                .ToListAsync();
            var orderResponse = new List<OrderOverviewResponseDTO>();
            orders.ForEach(o => orderResponse.Add(new OrderOverviewResponseDTO
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Product = o.OrderItems.Count > 1 ?
                    $"{o.OrderItems.First().Product.Title} and" +
                    $"{o.OrderItems.Count - 1} more...":
                    o.OrderItems.First().Product.Title,
                ProductImageUrl = o.OrderItems.First().Product.ImageUrl
            }));

            response.Data = orderResponse;

            return response;
        }

        public async Task<ServiceResponse<bool>> PalceOrder()
        {
            var products = (await _cartService.GetDbCartProduct()).Data;
            decimal totalPrice = 0;
            products.ForEach(product => totalPrice+=product.Price*product.Quantity);

            var orderItems = new List<OrderItem>();
            products.ForEach(product => orderItems.Add(new OrderItem
            {
                productId = product.ProductId,
                ProductTypeId = product.ProductTypeId,
                Quantity= product.Quantity,
                TotalPrice = product.Price*product.Quantity,
            }));
            var address = await _addressService.GetAddress();
            var order = new Order
            {
                UserId = _authService.GetUserId(),
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                OrderItems = orderItems,
                Name = address.Data.Name,
                Street= address.Data.Street,
                City= address.Data.City,
                State= address.Data.State,
                Pincode= address.Data.Pincode,
                Country= address.Data.Country,
        };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(_context.CartItems
                .Where(ci => ci.UserId == _authService.GetUserId()));
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true};
        }
    }
}
