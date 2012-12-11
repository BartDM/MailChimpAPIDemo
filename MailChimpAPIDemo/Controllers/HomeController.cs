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

        public ActionResult AddContact(string listId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(string listId, Models.Contact contact)
        {
            var subscribe = new PerceptiveMCAPI.Methods.listSubscribe();
            var input = new PerceptiveMCAPI.Types.listSubscribeInput()
            {
                parms = new PerceptiveMCAPI.Types.listSubscribeParms()
                {
                    email_address = contact.EmailAddress,
                    double_optin = false,
                    replace_interests = false,
                    send_welcome = false,
                    update_existing = true,
                    merge_vars = new Dictionary<string, object>(),
                    id = listId
                }
            };
            input.parms.merge_vars.Add("FNAME", contact.FirstName);
            input.parms.merge_vars.Add("LNAME", contact.LastName);
            input.parms.merge_vars.Add("BIRTHDATE", contact.BirthDate.HasValue ? contact.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty);

            PerceptiveMCAPI.Types.listSubscribeOutput output = subscribe.Execute(input);
            if (output != null)
            {
                if (output.result)
                {
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
            return View("Error");
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
