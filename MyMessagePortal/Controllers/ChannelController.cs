using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMessagePortal.EntityConfig;
using MyMessagePortal.Helpers;
using MyMessagePortal.Models;
using MyMessagePortal.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyMessagePortal.Controllers
{
    [Authorize]
    public class ChannelController : Controller
    {
        private readonly EFCContext _context;
        private readonly UserManager<UserModel> _userManager;

        public ChannelController(EFCContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var channels = _context.Channels.Include(x => x.CreatedBy).Include(x => x.ObservedChannels).ThenInclude(x => x.User).ToList();

            return View(channels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ChannelModel model)
        {
            TempData["Message"] = $"Dodawanie nowego kanału nie powiodło się";

            try
            {
                if (model.Id != 0)
                {
                    return BadRequest();
                }

                var currentUser = _userManager.Users.SingleOrDefault(x => x.UserName == User.Identity.Name);

                if (currentUser == null)
                {
                    throw new Exception("Nie udało się pobrać zalogowanego użytkownika");
                }

                if (ModelState.IsValid)
                {
                    model.DateAdded = DateTime.Now;
                    model.ChannelColor = ColorHelper.GetRandomColor();
                    model.IsDefault = false;
                    model.CreatedById = currentUser.Id;

                    _context.Channels.Add(model);
                    _context.SaveChanges();

                    TempData["Message"] = $"Dodawanie nowego kanału zakończyło się powodzeniem";

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var channel = _context.Channels.SingleOrDefault(x => x.Id == id);

            if (channel == null)
            {
                return BadRequest();
            }

            var currentUserId = _userManager.Users.SingleOrDefault(x => x.UserName == User.Identity.Name);

            if (currentUserId == null)
            {
                throw new Exception("Nie udało się pobrać bieżącego użytkownika");
            }

            if (channel.CreatedById != currentUserId.Id)
            {
                TempData["Message"] = "Nie możesz usuwać nie swoich kanałów";
                return RedirectToAction("Index");
            }

            if (channel.CreatedById == currentUserId.Id && channel.IsDefault)
            {
                TempData["Message"] = "Nie możesz usunąć swojego domyślnego kanału";
                return RedirectToAction("Index");
            }

            return View(channel);
        }

        [HttpPost]
        public IActionResult RemoveConfirm(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var channel = _context.Channels.SingleOrDefault(x => x.Id == id);

                if (channel == null)
                {
                    return BadRequest();
                }

                var currentUserId = _userManager.Users.SingleOrDefault(x => x.UserName == User.Identity.Name);

                if (currentUserId == null)
                {
                    throw new Exception("Nie udało się pobrać bieżącego użytkownika");
                }

                if (channel.CreatedById != currentUserId.Id)
                {
                    TempData["Message"] = "Nie możesz usuwać nie swoich kanałów";
                    return RedirectToAction("Index");
                }

                if (channel.CreatedById == currentUserId.Id && channel.IsDefault)
                {
                    TempData["Message"] = "Nie możesz usunąć swojego domyślnego kanału";
                    return RedirectToAction("Index");
                }

                _context.Channels.Remove(channel);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IActionResult> Observe(int id)
        {
            #region verison 1
            //var channel = _context.Channels.SingleOrDefault(x => x.Id == id);

            //var user = _userManager.Users.Where(x => x.UserName == User.Identity.Name).Include(x => x.ObservedChannels).ThenInclude(x => x.Channel)
            //    .SingleOrDefault();

            //if (channel == null || user == null)
            //{
            //    return BadRequest();
            //}

            //var isObserved = user.ObservedChannels.Any(x => x.UserId == user.Id && x.ChannelId == channel.Id);

            //if (isObserved)
            //{
            //    TempData["Message"] = $"Kanał: \"{channel.Name}\" jest już przez Ciebie obserwowany";
            //    return RedirectToAction("Index");
            //}

            //user.ObservedChannels.Add(new ObservedChannelsModel()
            //{
            //    ChannelId = id,
            //    UserId = user.Id
            //}); 

            //TempData["Message"] = $"Zaczołeś obserwować nowy kanał: \"{channel.Name}\".";
            //await _userManager.UpdateAsync(user);
            #endregion

            #region version 2
            try
            {
                var channel = _context.Channels.SingleOrDefault(x => x.Id == id);

                var user = await _userManager.GetUserAsync(User);

                if (channel == null || user == null)
                {
                    return BadRequest();
                }

                var isObserved = _context.ObservedChannels.Any(x => x.UserId == user.Id && x.ChannelId == channel.Id);

                if (isObserved)
                {
                    TempData["Message"] = $"Kanał: \"{channel.Name}\" jest już przez Ciebie obserwowany";
                    return RedirectToAction("Index");
                }

                _context.ObservedChannels.Add(new ObservedChannelsModel()
                {
                    UserId = user.Id,
                    ChannelId = channel.Id
                });

                _context.SaveChanges();

                TempData["Message"] = $"Zaczołeś obserwować nowy kanał: \"{channel.Name}\".";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            #endregion

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> StopObserving(int id)
        {
            #region verison 1
            //var channel = _context.Channels.SingleOrDefault(x => x.Id == id);

            //var user = _userManager.Users.Where(x => x.UserName == User.Identity.Name).Include(x => x.ObservedChannels).ThenInclude(x => x.Channel)
            //    .SingleOrDefault();

            //if (channel == null || user == null)
            //{
            //    return BadRequest();
            //}

            //var observed = user.ObservedChannels.SingleOrDefault(x => x.UserId == user.Id && x.ChannelId == channel.Id);

            //if (observed == null)
            //{
            //    TempData["Message"] = $"Kanał: \"{channel.Name}\" nie jest jeszcze przez Ciebie obserwowany";
            //    return RedirectToAction("Index");
            //}

            //if (channel.CreatedById == user.Id && channel.IsDefault)
            //{
            //    TempData["Message"] = $"Nie możesz nie obserwować swojego domyślnego kanału.";
            //    return RedirectToAction("Index");
            //}

            //user.ObservedChannels.Remove(observed);

            //TempData["Message"] = $"Nie obserwujesz już kanału: \"{channel.Name}\".";
            //await _userManager.UpdateAsync(user);
            #endregion

            #region version 2
            try
            {
                var channel = _context.Channels.SingleOrDefault(x => x.Id == id);

                var user = await _userManager.GetUserAsync(User);

                if (channel == null || user == null)
                {
                    return BadRequest();
                }

                var isObserved = _context.ObservedChannels.Any(x => x.UserId == user.Id && x.ChannelId == channel.Id);

                if (!isObserved)
                {
                    TempData["Message"] = $"Kanał: \"{channel.Name}\" nie jest jeszcze przez Ciebie obserwowany";
                    return RedirectToAction("Index");
                }

                if (channel.CreatedById == user.Id && channel.IsDefault)
                {
                    TempData["Message"] = $"Nie możesz nie obserwować swojego domyślnego kanału.";
                    return RedirectToAction("Index");
                }

                _context.ObservedChannels.Remove(new ObservedChannelsModel()
                {
                    UserId = user.Id,
                    ChannelId = channel.Id
                });

                _context.SaveChanges();

                TempData["Message"] = $"Nie obserwujesz już kanału: \"{channel.Name}\".";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            #endregion

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int channelId)
        {
            var channel = _context.Channels.Include(x => x.Messages).ThenInclude(x => x.CreatedBy).SingleOrDefault(x => x.Id == channelId);

            if (channel == null)
            {
                return BadRequest();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            return View(new ChannelViewModel() { Id = channel.Id, Name = channel.Name, Messages = channel.Messages });
        }

        public async Task<IActionResult> ObservedList(int page = 1, int itemsPerPage = 2)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var orderedChannels = _context.ObservedChannels
                .Include(x => x.Channel).ThenInclude(x => x.Messages).ThenInclude(x => x.CreatedBy)
                .Where(x => x.UserId == currentUser.Id)
                .OrderBy(x => !(x.Channel.IsDefault && x.Channel.CreatedById == currentUser.Id)).ThenBy(x => x.Channel.Name).ToList();

            var numOfPages = (int)Math.Ceiling((decimal)orderedChannels.Count / (decimal)itemsPerPage);

            page = page <= 0 ? 1 : page > numOfPages ? numOfPages : page;

            itemsPerPage = itemsPerPage <= 0 ? 2 : itemsPerPage;

            var channels = orderedChannels.Skip((page - 1)* itemsPerPage).Take(itemsPerPage);

            var model = new ObservedChannelsViewModel()
            {
                ObservedChannels = channels,
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                NumberOfPages = numOfPages
            };

            return View(model);
        }
    }
}
