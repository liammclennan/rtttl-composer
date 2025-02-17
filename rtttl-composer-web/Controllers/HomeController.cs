using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using rtttl_composer_library;
using rtttl_composer_web.Models;

namespace rtttl_composer_web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Return a model of the compiled tune to be interpreted by the client.
    /// </summary>
    [HttpPost]
    public IActionResult Compose([FromBody] PlayModel playModel)
    {
        return Json(Composer.RtttlToComposition(playModel.rtttl, 120));
    }
}

public class PlayModel
{
    public string rtttl { get; set; }
}