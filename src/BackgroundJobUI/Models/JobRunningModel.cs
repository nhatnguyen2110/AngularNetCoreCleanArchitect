using System.ComponentModel.DataAnnotations;
using BackgroundJobUI.Extensions;

namespace BackgroundJobUI.Models;

public class JobRunningModel
{
    [Required]
    [DataType(DataType.Date)]
    [RunningDate("Running Date should be not less than current date - 5 day")]
    public DateTime? RunningDate { get; set; }
    public string? Message { get; set; }
}
