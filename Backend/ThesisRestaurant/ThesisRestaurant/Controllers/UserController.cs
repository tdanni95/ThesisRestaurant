using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("user")]
    [AllowAnonymous]
    public class UserController : ApiController
    {
        /**
         * Cím hozzáadása
         * Cím módosítás
         * Cím törlés
         * User adat módosítás 
         * User level módosítás [admin only]
         * */
    }
}
