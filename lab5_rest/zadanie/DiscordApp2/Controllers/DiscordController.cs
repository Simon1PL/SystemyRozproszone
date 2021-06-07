using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DiscordApp2.Models;
using DiscordApp2.Services;

namespace DiscordApp2.Controllers
{
    public class DiscordController : Controller
    {
        private readonly ILogger<DiscordController> _logger;
        private readonly IDiscordService _discordService;

        public DiscordController(ILogger<DiscordController> logger, IDiscordService discordService)
        {
            _logger = logger;
            _discordService = discordService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Channels = await _discordService.GetAllChannels();
                var postsAmount = Enumerable.Range(1, 10).Select(x => (x * 100).ToString()).ToList();
                postsAmount.Add("MORE!");
                ViewBag.AvailablePostsAmount = postsAmount;
                return View();
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        public async Task<IActionResult> GetStats(ChannelModel model)
        {
            try
            {
                StatsModel statsModel = await _discordService.GetStats(model);
                return PartialView("~/Views/Discord/_wordStats.cshtml", statsModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
