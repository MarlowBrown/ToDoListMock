using Microsoft.EntityFrameworkCore;

namespace ToDoListMock.Models;

public class TicketContext : DbContext
{
    
    public TicketContext(DbContextOptions<TicketContext> options) : base(options)
    {
        
    }
    
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketStatus>().HasData(
            new TicketStatus() { TicketStatusId = "todo", Name = "To Do" },
            new TicketStatus() { TicketStatusId = "in_progress", Name = "In Progress" },
            new TicketStatus() { TicketStatusId = "qa", Name = "Quality Assurance" },
            new TicketStatus() { TicketStatusId = "done", Name = "Done" }
        );
    }
}