using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAppWebAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllOrigin")]
    public class ServiceTestController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet("GetTotalCount")]
        public int GetTotalCount()
        {
            return 5;
        }

        [HttpGet("GetRecords")]
        public string GetRecords()
        {
            string jsonData = "{\r\n  \"records\": [\r\n    {\r\n      \"Name\": \"Alfreds Futterkiste\",\r\n      \"City\": \"Berlin\",\r\n      \"Country\": \"Germany\"\r\n    },\r\n    {\r\n      \"Name\": \"Ana Trujillo Emparedados y helados\",\r\n      \"City\": \"México D.F.\",\r\n      \"Country\": \"Mexico\"\r\n    },\r\n    {\r\n      \"Name\": \"Antonio Moreno Taquería\",\r\n      \"City\": \"México D.F.\",\r\n      \"Country\": \"Mexico\"\r\n    },\r\n    {\r\n      \"Name\": \"Around the Horn\",\r\n      \"City\": \"London\",\r\n      \"Country\": \"UK\"\r\n    },\r\n    {\r\n      \"Name\": \"B's Beverages\",\r\n      \"City\": \"London\",\r\n      \"Country\": \"UK\"\r\n    },\r\n    {\r\n      \"Name\": \"Berglunds snabbköp\",\r\n      \"City\": \"Luleå\",\r\n      \"Country\": \"Sweden\"\r\n    },\r\n    {\r\n      \"Name\": \"Blauer See Delikatessen\",\r\n      \"City\": \"Mannheim\",\r\n      \"Country\": \"Germany\"\r\n    },\r\n    {\r\n      \"Name\": \"Blondel père et fils\",\r\n      \"City\": \"Strasbourg\",\r\n      \"Country\": \"France\"\r\n    },\r\n    {\r\n      \"Name\": \"Bólido Comidas preparadas\",\r\n      \"City\": \"Madrid\",\r\n      \"Country\": \"Spain\"\r\n    },\r\n    {\r\n      \"Name\": \"Bon app'\",\r\n      \"City\": \"Marseille\",\r\n      \"Country\": \"France\"\r\n    },\r\n    {\r\n      \"Name\": \"Bottom-Dollar Marketse\",\r\n      \"City\": \"Tsawassen\",\r\n      \"Country\": \"Canada\"\r\n    },\r\n    {\r\n      \"Name\": \"Cactus Comidas para llevar\",\r\n      \"City\": \"Buenos Aires\",\r\n      \"Country\": \"Argentina\"\r\n    },\r\n    {\r\n      \"Name\": \"Centro comercial Moctezuma\",\r\n      \"City\": \"México D.F.\",\r\n      \"Country\": \"Mexico\"\r\n    },\r\n    {\r\n      \"Name\": \"Chop-suey Chinese\",\r\n      \"City\": \"Bern\",\r\n      \"Country\": \"Switzerland\"\r\n    },\r\n    {\r\n      \"Name\": \"Comércio Mineiro\",\r\n      \"City\": \"São Paulo\",\r\n      \"Country\": \"Brazil\"\r\n    }\r\n  ]\r\n}";
            return jsonData;
        }

        [HttpGet("WelcomeMessage")]
        public string GetWelcomeMessage()
        {
            return "Hello";
        }
    }
}
