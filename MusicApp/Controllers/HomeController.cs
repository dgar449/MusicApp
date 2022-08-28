using Microsoft.AspNetCore.Mvc;
using MusicApp.Models;
using System.Diagnostics;
using MusicApp.Data;
using MusicApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISongRepo _songRepo;
        public DataClass dc= new DataClass();

        public HomeController(ISongRepo songRepo)
        {
            _songRepo = songRepo;
        }

        public ViewResult Index()
        {
            var model=_songRepo.GetAllSongs();
            return View(model); 
        }
        //public ViewResult Index1()
        //{
        //    var model = _songRepo.GetAllSongs();
        //    return View(model);
        //}
        public ViewResult ArtistList()
        {
            var model= _songRepo.GetArtists().ToList();
            ViewBag.ArtistName = new SelectList(model, "ArtistName", "ArtistName");
            //ViewBag.ArtistName=model;
            return View(model);
        }
        public ViewResult AlbumList()
        {
            var model = _songRepo.GetAlbum().ToList();
            ViewBag.ArtistName = new SelectList(model, "ArtistID", "ArtistName");
            return View(model);
        }
        public ViewResult AlbumResults(int sq)
        {
            var model = _songRepo.AllAlbums(sq);
            return View(model);
        }
        /* [HttpGet]
         public ViewResult SongList()
         {
             Song model = _songRepo.GetSongs(1);
             ViewBag.Song = model;
             ViewBag.PageTitle = "Song List";
             return View(); 
         }
         //[Route("Home/SongList/{id?}")]*/
        public ViewResult SongList(int id)
        {
            Song model = _songRepo.GetSongs(id);
            ViewBag.Song = model;
            ViewBag.PageTitle = "Song List";
            return View(model);
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
        public ViewResult Edit(int id)
        {
            Song model = _songRepo.GetSongs(id);
            ViewBag.Song = model;
            ViewBag.PageTitle = "Edit Song Details";
            return View(model);
        }
        public RedirectToActionResult Delete(int id)
        {
            var model = _songRepo.Delete(id);
            return RedirectToAction("Index");
        }
       public RedirectToActionResult Update(Song song)
        {
            var model = _songRepo.Update(song);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ViewResult Search(string sq)
        {
            var model = _songRepo.Search(sq);
            return View(model);
        }

        public ViewResult RankSongCount()
        {
            var model = _songRepo.RankSongsTotal();
            return View(model);
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