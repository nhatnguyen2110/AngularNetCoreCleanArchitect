using BackgroundJobUI.Models;
using BackgroundJobUI.Services;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundJobUI.Controllers;
[Authorize]
public class JobRunningController : Controller
{
    private readonly ILogger<JobRunningController> _logger;
    private readonly IJobHandlerService _jobHandlerService;
    public JobRunningController(ILogger<JobRunningController> logger
        , IJobHandlerService jobHandlerService
        )
    {
        _logger = logger;
        _jobHandlerService = jobHandlerService;
    }

    public IActionResult Index()
    {
        var model = new JobRunningModel() { RunningDate = DateTime.Now };
        return View(model);
    }
    [HttpPost]
    public IActionResult Index(JobRunningModel model)
    {
        if (ModelState.IsValid)
        {
            BackgroundJob.Enqueue(() => _jobHandlerService.ExecuteUpdateHistoricalWeather(null, model.RunningDate));
            model.Message = "Enqueue job successfully";
        }

        return View(model);
    }
}
