using KoronaMetre.Areas.Admin.Models;
using KoronaMetre.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace KoronaMetre.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly KoronaContextDb _db;

        public DashboardController(KoronaContextDb context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            var ulkeler = _db.KoronaBilgileri
                .OrderByDescending(x => x.VakaSayisi)
                .Select(x => x.Ulke.Ad)
                .ToArray();
            var vakalar = _db.KoronaBilgileri
                .OrderByDescending(x => x.VakaSayisi)
                .Select(x => x.VakaSayisi)
                .ToArray();
            var olumler = _db.KoronaBilgileri
                .OrderByDescending(x => x.VakaSayisi)
                .Select(x => x.OlumSayisi)
                .ToArray();


            var vm = new DashboardViewModel()
            {
                OlumSayilariJson=JsonSerializer.Serialize(olumler),
                UlkelerJson=JsonSerializer.Serialize(ulkeler),
                VakaSayilariJson = JsonSerializer.Serialize(vakalar)
            };
            return View(vm);
        }
    }
}
