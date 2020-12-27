using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using System.Net.Http;

namespace MVC.Controllers
{
    public class QueueController : Controller
    {
        private Booking_SystemDBEntities2 db = new Booking_SystemDBEntities2();

        public ActionResult Queue_a_notification(int id=0)
        {
            return View(new Queue());
        }
        [HttpPost]
        public ActionResult Queue_a_notification(Queue q)
        {
            HttpResponseMessage response = Globalvariables.WebApiClient.PostAsJsonAsync("Queue", q).Result;
            return RedirectToAction("Index");
        }
        // GET: Queue
        public async Task<ActionResult> Index()
        {
            return View(await db.Queues.ToListAsync());
        }

        // GET: Queue/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queue queue = await db.Queues.FindAsync(id);
            if (queue == null)
            {
                return HttpNotFound();
            }
            return View(queue);
        }

        // GET: Queue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Queue/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,notification_context")] Queue queue)
        {
            if (ModelState.IsValid)
            {
                db.Queues.Add(queue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(queue);
        }

        // GET: Queue/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queue queue = await db.Queues.FindAsync(id);
            if (queue == null)
            {
                return HttpNotFound();
            }
            return View(queue);
        }

        // POST: Queue/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,notification_context")] Queue queue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(queue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(queue);
        }

        // GET: Queue/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queue queue = await db.Queues.FindAsync(id);
            if (queue == null)
            {
                return HttpNotFound();
            }
            return View(queue);
        }

        // POST: Queue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Queue queue = await db.Queues.FindAsync(id);
            db.Queues.Remove(queue);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
