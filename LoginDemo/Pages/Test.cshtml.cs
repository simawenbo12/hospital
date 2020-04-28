using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages
{
    [IgnoreAntiforgeryToken]
    [BindProperties]
    public class TestModel : PageModel
    {

        public Te GetT { get; set; }
        public int ID { get; set; }
        public string  Name { get; set; }

        public IActionResult OnPostPostClass()
        {
            return new JsonResult("Te="+GetT.Name + " " + GetT.ID);
        }
        public IActionResult OnPostPostQuery()
        {
            return  new JsonResult("Query"+ID+" "+Name);
        }
        public IActionResult OnPostPostStr(string data)
        {
            return new JsonResult("Str = "+data);
        }
    }
    public class Te
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}