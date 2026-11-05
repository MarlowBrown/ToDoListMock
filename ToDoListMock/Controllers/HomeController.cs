using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListMock.Models;

namespace ToDoListMock.Controllers;

public class HomeController : Controller
{
    private TicketContext context;
    public HomeController(TicketContext ctx) => context = ctx;
    
    public IActionResult Index(string id)
    {
        var filters = new Filters(id);
        ViewBag.Filters = filters;
        ViewBag.Statuses = context.TicketStatuses.ToList();
        ViewBag.DueFilters = Filters.DueFilterValues;
        
        IQueryable<Ticket> query = context.Tickets.Include(t => t.Status);
        if (filters.HasStatus) {
            query = query.Where(t => t.TicketStatusId == filters.StatusId);
        }
        if (filters.HasDue) {
            var today = DateTime.Today;
            if (filters.IsPast)
                query = query.Where(t => t.DueDate < today);
            else if (filters.IsFuture)
                query = query.Where(t => t.DueDate > today);
            else if (filters.IsToday)
                query = query.Where(t => t.DueDate == today);
        }
        var tasks = query.OrderBy(t => t.DueDate).ToList();
        return View(tasks);
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.Statuses = context.TicketStatuses.ToList();
        return View();
    }
    
    // I guess this weird binding is required if I dont want to bind the Tickets Id? Kinda gross tbh
    [HttpPost]
    public IActionResult Add([Bind("Name,Description,DueDate,SprintNumber,PointValue,TicketStatusId")] Ticket ticket)
    {
        if (ModelState.IsValid){
            context.Tickets.Add(ticket);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        else {
            ModelState.AddModelError("", "There are errors in the form");
            ViewBag.Statuses = context.TicketStatuses.ToList();
            return View(ticket);
        }
    }
    
    [HttpPost]
    public IActionResult Filter(string[] filter)
    {
        string id = string.Join('-', filter);
        return RedirectToAction("Index", new { ID = id });
    }
    
    [HttpPost]
    public IActionResult Edit([FromRoute] string id, Ticket selected)
    {
        if (selected.TicketStatusId == null) {
            context.Tickets.Remove(selected);
        }
        else {
            string newStatusId = selected.TicketStatusId;
            selected = context.Tickets.Find(selected.Id);
            selected.TicketStatusId = newStatusId;
            context.Tickets.Update(selected);
        }
        context.SaveChanges();

        return RedirectToAction("Index", new { ID = id });
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