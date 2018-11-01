using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMessagePortal.EntityConfig;
using MyMessagePortal.Models;
using MyMessagePortal.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyMessagePortal.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly EFCContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly IHttpContextFactory _httpContextFactory;

        public MessageController(EFCContext context, UserManager<UserModel> userManager, IHttpContextFactory httpContextFactory)
        {
            _context = context;
            _userManager = userManager;
            _httpContextFactory = httpContextFactory;
        }

        public IActionResult Index(int page = 1, int itemsPerPage = 10)
        {
            TempData["NumMessagesDisplayed"] = itemsPerPage;

            var orderedMessages = _context.Messages.Include(x => x.Channel).Include(x => x.CreatedBy)
                .OrderBy(x => x.DateAdded).Select(x => new MessageViewModel()
                {
                    Id = x.Id,
                    Text = x.Text,
                    DateAdded = x.DateAdded,
                    CreatedBy = x.CreatedBy,
                    CreatedById = x.CreatedById,
                    Channel = x.Channel,
                    ChannelId = x.ChannelId
                }).ToList();

            var numOfPages = (int)Math.Ceiling((decimal)orderedMessages.Count / (decimal)itemsPerPage);

            page = page <= 0 ? 1 : page > numOfPages ? numOfPages : page;

            itemsPerPage = itemsPerPage <= 0 ? 2 : itemsPerPage;

            orderedMessages = orderedMessages.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            return View(orderedMessages);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int channelId)
        {
            var channel = _context.Channels.SingleOrDefault(x => x.Id == channelId);

            if (channel == null && channelId != 0)
            {
                return BadRequest();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (channelId == 0)
            {
                var channels = _context.Channels.Select(x => new { x.Id, x.Name }).ToList();
                ViewBag.ChannelsList = channels;
            }

            //ViewBag.ReturnUrl = await GetValidUrl(returnUrl, "GET");

            ViewBag.ReturnUrl = Request.Query["returnUrl"].ToString();

            return View(new MessageViewModel() { Channel = channel, ChannelId = channelId, CreatedById = currentUser.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Add(MessageViewModel model)
        {
            //returnUrl = await GetValidUrl(returnUrl, "POST");
            //ViewBag.ReturnUrl = returnUrl;
            var returnUrl = Request.Form["returnUrl"].ToString();
            var query = Request.Query;

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id != 0)
                    {
                        return BadRequest();
                    }
                
                    var channel = _context.Channels.SingleOrDefault(x => x.Id == model.ChannelId);

                    if (channel == null)
                    {
                        return BadRequest();
                    }

                    var currentUser = await _userManager.GetUserAsync(User);

                    if (model.CreatedById != currentUser.Id)
                    {
                        TempData["Message"] = "Niespójność użytkowników w modelu";
                        throw new Exception("Niespójność użytkowników w modelu");
                    }

                    var newMessage = new MessageModel()
                    {
                        Id = model.Id,
                        Text = model.Text,
                        DateAdded = DateTime.Now,
                        ChannelId = model.ChannelId,
                        CreatedById = model.CreatedById
                    };

                    _context.Messages.Add(newMessage);
                    _context.SaveChanges();

                    TempData["Message"] = $"Dodano nową wiadomość do kanału: {channel.Name}";

                    //return RedirectToAction("Index");
                    return Redirect(returnUrl);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id, string returnUrl = "/Message/Index")
        {
            returnUrl = GetValidUrl(returnUrl, "GET");
            ViewBag.ReturnUrl = returnUrl;

            if (id <= 0)
            {
                return BadRequest();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                TempData["Message"] = "Nie udało się pobrać danych aktualnego użytkownika";
                throw new Exception("Nie udało się pobrać danych aktualnego użytkownika");
            }

            var message = _context.Messages.Include(x => x.Channel).SingleOrDefault(x => x.Id == id);

            if (message == null)
            {
                return BadRequest();
            }

            if (currentUser.Id != message.CreatedById)
            {
                TempData["Message"] = "Nie możesz usuwać nie swoich wiadomości";
                //return RedirectToAction("Index");
                return Redirect(returnUrl);
            }

            var now = DateTime.Now;

            if (Math.Abs((now - message.DateAdded).TotalMinutes) > 9)
            {
                TempData["Message"] = "Nie możesz usuwać wiadomości po upływie 10 minut";
                //return RedirectToAction("Index");
                return Redirect(returnUrl);
            }

            var model = new MessageViewModel()
            {
                Id = id,
                Channel = message.Channel,
                ChannelId = message.ChannelId,
                CreatedBy = message.CreatedBy,
                CreatedById = currentUser.Id,
                DateAdded = message.DateAdded,
                Text = message.Text
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveConfirm(int id, string returnUrl = "/Message/Index")
        {
            returnUrl = GetValidUrl(returnUrl, "POST");
            ViewBag.ReturnUrl = returnUrl;

            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    TempData["Message"] = "Nie udało się pobrać danych aktualnego użytkownika";
                    throw new Exception("Nie udało się pobrać danych aktualnego użytkownika");
                }

                var message = _context.Messages.Include(x => x.Channel).SingleOrDefault(x => x.Id == id);

                if (message == null)
                {
                    return BadRequest();
                }

                if (currentUser.Id != message.CreatedById)
                {
                    TempData["Message"] = "Nie możesz usuwać nie swoich wiadomości";
                    //return RedirectToAction("Index");
                    return Redirect(returnUrl);
                }

                var now = DateTime.Now;

                if (Math.Abs((now - message.DateAdded).TotalMinutes) > 9)
                {
                    TempData["Message"] = "Nie możesz usuwać wiadomości po upływie 10 minut";
                    //return RedirectToAction("Index");
                    return Redirect(returnUrl);
                }

                _context.Messages.Remove(message);
                _context.SaveChanges();

                TempData["Message"] = $"Usunąłeś wiadomość z kanału {message.Channel.Name}";

                //return RedirectToAction("Index");
                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private bool ValidateUrl(string path, string method)
        {
            //IRouteCollection router = RouteData.Routers.OfType<IRouteCollection>().First();

            //HttpContext context = _httpContextFactory.Create(HttpContext.Features);
            //context.Request.Path = path;
            //context.Request.Method = method;

            //var routeContext = new RouteContext(context);
            //await router.RouteAsync(routeContext);

            //bool exists = routeContext.Handler != null;

            //return exists;
            return true;
        }

        private string GetValidUrl(string path, string method)
        {
            var isValid = ValidateUrl(path, method);
            return isValid ? path : "/Message/Index";
        }
    }
}
