using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToDoListMock.Models.Attributes;

namespace ToDoListMock.Models;

public class Ticket
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please enter a ticket name")]
    [MinimumStringLengthValidation(5)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Please enter a due date")]
    [FutureDateValidation]
    public DateTime? DueDate { get; set; }
    
    [Required(ErrorMessage = "Please enter a description")]
    [MinimumStringLengthValidation(10)]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Please enter a sprint number")]
    [MinimumIntegerValidation(1)]
    public int SprintNumber { get; set; }
    
    [Required(ErrorMessage = "Point value required for ticket")]
    [Range(1, 10, ErrorMessage = "Point value must be 1 to 10")]
    public int PointValue { get; set; }
    
    [Required(ErrorMessage = "Please select a status")]
    public string TicketStatusId { get; set; }
    
    public TicketStatus? Status { get; set; }

    public bool Overdue => Status?.TicketStatusId.ToLower() != "done" && DueDate < DateTime.Today;
    
}