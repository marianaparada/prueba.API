using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.API.Data;
using prueba.API.Model;

namespace prueba.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealsController : ControllerBase
{
    private readonly AppDbContext _context;

    public MealsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/meals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meals>>> GetMeals()
    {
        return await _context.Meals.AsNoTracking().ToListAsync();
    }

    // GET: api/meals/{name}
    [HttpGet("{name}")]
    public async Task<ActionResult<Meals>> GetMeal(string name)
    {
        var meal = await _context.Meals.FindAsync(name);
        if (meal == null) return NotFound();
        return meal;
    }

    // POST: api/meals
    [HttpPost]
    public async Task<ActionResult<Meals>> CreateMeal(Meals meal)
    {
        // Si ya existe el PK (Name), 409 Conflict
        bool exists = await _context.Meals.AnyAsync(m => m.Name == meal.Name);
        if (exists) return Conflict(new { message = $"Meal '{meal.Name}' ya existe." });

        _context.Meals.Add(meal);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMeal), new { name = meal.Name }, meal);
    }

    // PUT: api/meals/{name}
    [HttpPut("{name}")]
    public async Task<IActionResult> UpdateMeal(string name, Meals meal)
    {
        if (!string.Equals(name, meal.Name, StringComparison.Ordinal))
            return BadRequest(new { message = "El 'name' de la ruta debe coincidir con meal.Name." });

        // Attach y marcar como modificado para actualizar todas las props
        _context.Entry(meal).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            bool exists = await _context.Meals.AnyAsync(m => m.Name == name);
            if (!exists) return NotFound();
            throw; // si fue otra excepción de concurrencia, la relanzamos
        }

        return NoContent();
    }

    // DELETE: api/meals/{name}
    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteMeal(string name)
    {
        var meal = await _context.Meals.FindAsync(name);
        if (meal == null) return NotFound();

        _context.Meals.Remove(meal);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
