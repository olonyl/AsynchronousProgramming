using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AsynchronousProgramming.API3.Controllers
{
    public class HomeController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            using (var client = new HttpClient())
            {
                var httpMessage = await client.GetAsync("https://feeds.feedburner.com/fekberg");

                var content = await httpMessage.Content.ReadAsStringAsync();

                return Ok(content);
            }

        }
    }
}
