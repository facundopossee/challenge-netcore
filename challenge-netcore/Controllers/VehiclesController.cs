using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using challenge_netcore.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace challenge_netcore.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public ActionResult Index()
        {
            return View(nameof(Index), _context.Vehicles.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = _context.Vehicles
                .FirstOrDefault(m => m.ID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(nameof(Details), vehicle);
        }

        // GET: Vehicles/Create
        public async Task<IActionResult> Create()
        {
            List<string> owners = await GetOwners();
            ViewData["owners"] = owners;
            return View(nameof(Create));
        }

        public async Task<List<string>> GetOwners()
        {
            string baseurl = "https://reqres.in/api/users";
            var data = new List<string>();

            for (int i = 1; i <= 3; i++) //pages
            {
                var response = await ApiCall(baseurl + "?page=" + i);
                JObject responseObj = JObject.Parse(response);
                var dataArray = responseObj["data"].Value<JArray>();
                foreach (var item in dataArray)
                {
                    data.Add(item["first_name"].ToString() + " " + item["last_name"].ToString());
                }
            }
            return data;
        }

        //Call to api reqres.in
        [HttpGet]
        private async Task<dynamic> ApiCall(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }


        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ID,LicensePlate,Brand,Model,Doors,Owner")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                try
                {
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle =  _context.Vehicles
                .FirstOrDefault(m => m.ID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(nameof(Delete), vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var vehicle =  _context.Vehicles.FirstOrDefault(v => v.ID == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.ID == id);
        }
    }
}
