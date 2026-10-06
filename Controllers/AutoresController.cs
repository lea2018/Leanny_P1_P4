using Leanny_P1_P4.Models;
using Leanny_P1_P4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Leanny_P1_P4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutoresController : ControllerBase
{
    private readonly AutoresService _service;

    public AutoresController(AutoresService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Autor autor)
    {
        var id = await _service.SaveAsync(autor);

        autor.IdAutor = id;

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            autor);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var autores = await _service.GetListAsync();

        return Ok(autores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var autor = await _service.GetByIdAsync(id);

        if (autor is null)
        {
            return NotFound();
        }

        return Ok(autor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Autor autor)
    {
        var actualizado = await _service.UpdateAsync(id, autor);

        if (!actualizado)
        {
            return NotFound();
        }

        autor.IdAutor = id;

        return Ok(autor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _service.DeleteAsync(id);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }
}