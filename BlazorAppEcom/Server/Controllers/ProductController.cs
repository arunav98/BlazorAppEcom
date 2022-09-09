using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppEcom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /*private static List<Product> Products = new List<Product>{
            new Product{
                Id = 1,
                Title ="2 States: The Story of My Marriage",
                Description ="2 States: The Story of My Marriage commonly known as 2 States is a 2009 novel written by Chetan Bhagat.It is the story about a couple coming from two states in India, who face hardships in convincing their parents to approve of their marriage. Bhagat wrote this novel after quitting his job as an investment banker.",
                ImageUrl="https://upload.wikimedia.org/wikipedia/en/3/3a/2_States_-_The_Story_Of_My_Marriage.jpg",
                Price = 100
            },
            new Product{
                Id = 2,
                Title ="The 3 Mistakes of My Life",
                Description = "The 3 Mistakes of My Life is the third novel written by Chetan Bhagat. The book was published in May 2008 and had an initial print-run of 420,000. The novel follows the story of three friends and is based in the city of Ahmedabad in western India. This is the third best seller novel by Chetan Bhagat.",
                ImageUrl="https://upload.wikimedia.org/wikipedia/en/1/1d/3_Mistakes_of_My_Life.jpg",
                Price = 107
            },
            new Product{
                Id = 3,
                Title ="Half Girlfriend",
                Description = "Half Girlfriend is an Indian English coming of age, young adult romance novel by Indian author Chetan Bhagat.[1] The novel, set in rural Bihar, New Delhi, Patna, and New York, is the story of a Bihari boy in quest of winning over the girl he loves. This is Bhagat's sixth novel which was released on 1 October 2014[4] by Rupa Publications.",
                ImageUrl="https://upload.wikimedia.org/wikipedia/en/c/c6/Half_Girlfriend.jpg",
                Price = 108
            },
        };*/

        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProduct()
        {
            var products = await _dataContext.Products.ToListAsync();
            return Ok(products);
        }
    }
}
