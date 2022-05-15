using Microsoft.AspNetCore.Mvc;
using MusicApp.Models;
using System.Diagnostics;

namespace MusicApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISongRepo _songRepo;

        public HomeController(ISongRepo songRepo)
        {
            _songRepo = songRepo;
        }

        public ViewResult Index()
        {
            var model=_songRepo.GetAllSongs();
            return View(model); 
        }

        public ViewResult SongList(int id)
        {
            Song model = _songRepo.GetSongs(id);
            ViewBag.Song = model;
            ViewBag.PageTitle = "Song List";
            return View(); 
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public RedirectToActionResult Create(Song song)
        {
            Song newSong = _songRepo.Add(song);
            return RedirectToAction("SongList", new {id =newSong.SongID});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}