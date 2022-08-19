using System.ComponentModel.DataAnnotations;

namespace entry_task.Models;

public class Event
{
    [Required] [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public long CreatedAt { get; init; }
}

public class EventQueryParams: IValidatableObject
{
    [Required][MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int From { get; set; }
    [Required]
    public int To { get; set; }
    [Required]
    public int Interval { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (From > DateTimeOffset.Now.ToUnixTimeSeconds())
        {
            yield return new ValidationResult("Parameter From can not be set in the future");
        }

        if (From >= To)
        {
            yield return new ValidationResult("Parameter To must be greater than parameter From");
        }

        if (Interval > (To - From))
        {
            yield return new ValidationResult("Parameter Interval is greater than the time range provided");
        }

        if (Interval <= 0)
        {
            yield return new ValidationResult("Parameter Interval must be greater than 0");
        }
    }
}