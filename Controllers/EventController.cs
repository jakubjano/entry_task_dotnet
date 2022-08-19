using System.ComponentModel.DataAnnotations;
using entry_task.Models;
using entry_task.Repository;
using Microsoft.AspNetCore.Mvc;

namespace entry_task.Controllers;
[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly EventRepo _eventRepo;

    public EventController(EventRepo eventRepo)
    {
        _eventRepo = eventRepo;
    }
    
    [HttpGet("/event")]
    public ActionResult GetAggregatedEvents([FromQuery] EventQueryParams queryParams)
    {
        var events = 
            _eventRepo.GetAggregatedEvents(queryParams.Name, queryParams.From, queryParams.To, queryParams.Interval);
        if (events.Count == 0)
        {
            return NotFound("No events found with the given name");
        }
        return Ok(events);
    }

    [HttpPost("/event")]
    public ActionResult PostEvent([FromQuery][Required][MaxLength(100)] string name)
    {
        var e = _eventRepo.Create(name);
        return Ok(e);
    }
}