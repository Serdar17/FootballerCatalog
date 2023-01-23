using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Web.Context;
using Web.Dto;
using Web.Models;
using Web.Models.Repository.Interfaces;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly IMapper _mapper;
    private readonly IPlayerManager _playerManager;
    private readonly IHubContext<PlayerHub> _hub;

    public HomeController(IMapper mapper, 
        IPlayerManager playerManager, IHubContext<PlayerHub> hub)
    {
        _mapper = mapper;
        _playerManager = playerManager;
        _hub = hub;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var players = _playerManager.GetPlayers();
        return View(players);
    }
    
    [HttpGet("create")]
    public IActionResult Create()
    {
        var playerDto = new PlayerDto();
        var names = _playerManager.GetPlayers().Select(item => item.TeamName).ToList();
        playerDto.TeamNames = names;
        return View(playerDto);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] PlayerDto playerDto)
    {
        if (ModelState.IsValid)
        {
            var player = _mapper.Map<Player>(playerDto);
            var response = await _playerManager.CreateAsync(player);
            if (response.StatusCode == 200)
            {
                await SendPlayersAsync();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("FirstName", response.Description);
        }
        return View(playerDto);
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult Edit([FromRoute] int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        var playerDto = _mapper.Map<PlayerDto>(_playerManager.GetPlayerById(id));
        playerDto.TeamNames = _playerManager.GetPlayers().Select(item => item.TeamName).ToList();
        ViewBag.Id = id;
        return View(playerDto);
    }

    [HttpPost("edit/{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int? id, [FromForm] PlayerDto playerDto)
    {
        if (id is null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            var player = _mapper.Map<Player>(playerDto);
            _playerManager.UpdatePlayer(id, player);
            await SendPlayersAsync();
            return RedirectToAction("Index");
        }
        ViewBag.Id = id;
        return View(playerDto);
    }

    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        _playerManager.DeletePlayer(id);
        await SendPlayersAsync();
        return RedirectToAction("Index");
    }

    [NonAction]
    private async Task SendPlayersAsync()
    {
        var players = _playerManager.GetPlayers();
        await _hub.Clients.All.SendAsync("Send", players);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}