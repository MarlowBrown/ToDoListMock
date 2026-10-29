using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToDoListMock.Models;

public class Ticket
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please enter a ticket name")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Please enter a due date")]
    public DateTime? DueDate { get; set; }
    
    [Required(ErrorMessage = "Please enter a description")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Please enter a sprint number")]
    public int SprintNumber { get; set; }
    
    [Required(ErrorMessage = "Point value required for ticket")]
    public int PointValue { get; set; }
    
    [Required(ErrorMessage = "Please select a status")]
    public string TicketStatusId { get; set; }
    
    public TicketStatus? Status { get; set; }

    public bool Overdue => Status?.TicketStatusId.ToLower() != "done" && DueDate < DateTime.Today;
    
}