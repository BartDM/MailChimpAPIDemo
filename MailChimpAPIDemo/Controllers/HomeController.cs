using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PerceptiveMCAPI;

namespace MailChimpAPIDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var returnValue = new List<McList>();

            var lists = new PerceptiveMCAPI.Methods.lists();
            var input = new PerceptiveMCAPI.Types.listsInput();

            PerceptiveMCAPI.Types.listsOutput output = lists.Execute(input);
            if (output != null && output.result.Any())
            {
                foreach (var listsResult in output.result)
                {
                    returnValue.Add(new McList()
                        {
                            ListId = listsResult.id,
                            ListName = listsResult.name,
                            MemberCount = listsResult.member_count,
                            UnsubscribeCount = listsResult.unsubscribe_count
                        });
                }
            }

            return View(returnValue);
        }

    }

    public class McList
    {
        public string ListId { get; set; }
        public string ListName { get; set; }
        public int MemberCount { get; set; }
        public int UnsubscribeCount { get; set; }
    }
}
