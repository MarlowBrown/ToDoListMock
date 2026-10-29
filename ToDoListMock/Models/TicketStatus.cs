using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListMock.Models;

public class TicketStatus
{
    public string TicketStatusId { get; set; }
    public string Name { get; set; }
}