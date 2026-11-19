using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListMock.Controllers;
using ToDoListMock.Models;

namespace ToDoListMockTests;

public class UnitTest1
{
private static List<Ticket> SampleTickets()
    {
        var today = DateTime.Today;

        return new List<Ticket>
        {
            new Ticket { Id = 1, Name = "Past Todo",     DueDate = today.AddDays(-2), TicketStatusId = "todo" },
            new Ticket { Id = 2, Name = "Today In Progress",  DueDate = today,             TicketStatusId = "in_progress" },
            new Ticket { Id = 3, Name = "Future QA",     DueDate = today.AddDays(3),  TicketStatusId = "qa" },
            new Ticket { Id = 4, Name = "Past Done",     DueDate = today.AddDays(-1), TicketStatusId = "done" },
            new Ticket { Id = 5, Name = "Future Todo",   DueDate = today.AddDays(1),  TicketStatusId = "todo" }
        };
    }

    private static List<Ticket> ApplyFilters(IEnumerable<Ticket> tickets, string filterString)
    {
        var filters = new Filters(filterString);
        var query = tickets.AsEnumerable();

        if (filters.HasStatus)
            query = query.Where(t => t.TicketStatusId == filters.StatusId);

        if (filters.HasDue)
        {
            var today = DateTime.Today;
            if (filters.IsPast)
                query = query.Where(t => t.DueDate < today);
            else if (filters.IsFuture)
                query = query.Where(t => t.DueDate > today);
            else if (filters.IsToday)
                query = query.Where(t => t.DueDate == today);
        }

        return query.OrderBy(t => t.DueDate).ToList();
    }

    [Fact]
    public void FilterAll()
    {
        var result = ApplyFilters(SampleTickets(), "all-all");

        Assert.Equal(5, result.Count);
        Assert.Equal("Past Todo", result[0].Name);
        Assert.Equal("Future QA", result[^1].Name);
    }

    [Fact]
    public void ReturnMatchingStatus()
    {
        var result = ApplyFilters(SampleTickets(), "all-todo");

        Assert.Equal(2, result.Count);
        Assert.All(result, t => Assert.Equal("todo", t.TicketStatusId));
    }

    [Fact]
    public void ReturnPastTickets()
    {
        var result = ApplyFilters(SampleTickets(), "past-all");

        Assert.Equal(2, result.Count);
        Assert.All(result, t => Assert.True(t.DueDate < DateTime.Today));
    }

    [Fact]
    public void ReturnTodaysTickets()
    {
        var result = ApplyFilters(SampleTickets(), "today-all");

        Assert.Single(result);
        Assert.Equal("Today In Progress", result[0].Name);
    }

    [Fact]
    public void ReturnFutureTickets()
    {
        var result = ApplyFilters(SampleTickets(), "future-all");

        Assert.Equal(2, result.Count);
        Assert.All(result, t => Assert.True(t.DueDate > DateTime.Today));
    }
    
    
}