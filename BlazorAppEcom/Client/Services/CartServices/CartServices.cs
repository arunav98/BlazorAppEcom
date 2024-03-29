﻿using BlazorAppEcom.Client.Pages;
using BlazorAppEcom.Client.Services.AuthService;
using BlazorAppEcom.Shared;
using Blazored.LocalStorage;

namespace BlazorAppEcom.Client.Services.CartServices
{
    public class CartServices : ICartService
    {
        private ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public CartServices(ILocalStorageService localStorage,HttpClient http
            ,IAuthService authService)
        {
            _localStorage = localStorage;
            _http = http;
            _authService = authService;
        }
        public event Action onChange;
        public async Task AddToCart(CartItem cartItem)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/cart/add", cartItem);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                {
                    cart = new List<CartItem>();
                }
                var sameItem = cart.Find(p => p.ProductId == cartItem.ProductId && p.ProductTypeId == cartItem.ProductTypeId);
                if (sameItem == null)
                    cart.Add(cartItem);
                else
                    sameItem.Quantity += cartItem.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
                onChange.Invoke();
            }
            await GetItemCount();
        }

       

        public async Task<List<CartItem>> GetAll()
        {
            if (await _authService.IsUserAuthenticated()) { return new List<CartItem>(); }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                return cart;
            }
        }

        public async Task<List<CartProductResponseDTO>> GetCartProducts()
        {
            if (await _authService.IsUserAuthenticated()) {
                var response = await _http.GetFromJsonAsync < ServiceResponse<List<CartProductResponseDTO>>>("api/cart");
                return response.Data;
            }
            else
            {
                var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cartItems == null)
                {
                    return new List<CartProductResponseDTO>();
                }
                var response = await _http.PostAsJsonAsync("api/cart/products", cartItems);
                var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponseDTO>>>();
                return cartProducts.Data;
            }
            
        }

        public async Task RemoveCartItem(int productId, int productVariantId)
        {
            if(await _authService.IsUserAuthenticated())
            {
                await _http.DeleteAsync($"api/cart/{productId}/{productVariantId}");
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                {
                    return;
                }
                var cartItem = cart.Find(x => x.ProductId == productId && x.ProductTypeId == productVariantId);
                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                    await _localStorage.SetItemAsync("cart", cart);
                    await GetItemCount();
                }
            }
                
                

        }

        public async Task UpdateQuantity(CartProductResponseDTO product)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/cart/update", product);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                {
                    return;
                }
                var cartItem = cart.Find(x => x.ProductId == product.ProductId && x.ProductTypeId == product.ProductTypeId);
                if (cartItem != null)
                {
                    cartItem.Quantity = product.Quantity;
                    await _localStorage.SetItemAsync("cart", cart);
                    onChange.Invoke();
                }
            }

        }

        public async Task StoreCartItem(bool emptyLocalCart = false)
        {
            var localCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (localCart == null)
                return;
            await _http.PostAsJsonAsync("api/cart",localCart);
            await _localStorage.RemoveItemAsync("cart");
        }

        public async Task GetItemCount()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
                int count = result.Data;
                await _localStorage.SetItemAsync("cartItemsCount", count);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                await _localStorage.SetItemAsync<int>("cartItemsCount", cart != null? cart.Count:0);

            }
            onChange.Invoke();
        }
    }
}
