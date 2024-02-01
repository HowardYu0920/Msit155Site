using Microsoft.AspNetCore.Mvc;
using Msit155Site.Models;
using System.Text;

namespace Msit155Site.Controllers
{
    public class ApiController : Controller
    {

        private readonly MyDBContext _context;
        public ApiController(MyDBContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            Thread.Sleep(50000);
            //int x = 10;
            //int y = 0;
            //int z = x / y;
            return Content("Content, Hello！","text/plain",Encoding.UTF8);
        }

        //讀取城市
        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(x => x.City).Distinct();
            return Json(cities);
        }
        //根據城市讀取鄉政區
        public IActionResult District(string city)
        {
            var district = _context.Addresses.Where(c=>c.City == city).Select(x => x.SiteId).Distinct();
            return Json(district);
        }
        //根據鄉政區讀取路名
        public IActionResult Road(string site_id = "")
        {
            var road = _context.Addresses.Where(c => c.SiteId == site_id).Select(x => x.Road).Distinct();
            return Json(road);
        }


        public IActionResult Avatar(int id=1)
        {
            Member? member = _context.Members.Find(id);
            if (member != null)
            {
                byte[] img = member.FileData;
                if (img != null)
                {
                    return File(img,"image/jpeg");
                }
            }
            return NotFound();
        }

        public IActionResult Register(string name ,string email ,int age )
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "guest";
            }

            return Content($"Hello {name}.Your email is {email},and You are { age}years old！");
        }


        public IActionResult CheckAccount(int id)
        {
            return View();
        }

        public IActionResult CheckUserNameExist(string name)
        {
            var member = _context.Members.Any(m => m.Name == name);
            if (member != false)
            {
                return Content("帳號已存在!", "text/plain", Encoding.UTF8);
            }
            return  Content("帳號可使用!", "text/plain", Encoding.UTF8);
        }

        //private bool MemberCheck(string name)
        //{
        //    var Member = _context.Members.FirstOrDefault(m => m.Name == name);
        //    return Member != null;
        //}

    }
}
