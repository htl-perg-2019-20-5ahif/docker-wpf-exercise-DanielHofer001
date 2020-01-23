using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookAPI;
using System.Text.Json;
namespace BookAPI.Controllers
{
    [Route("car")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly DataContext _context;

        public CarsController(DataContext context)
        {
            _context = context;
          
        }


        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            if (_context.Cars.Count() == 0)
            {
                var data = await System.IO.File.ReadAllTextAsync("makes_models.json");
                var cars = JsonSerializer.Deserialize<IEnumerable<Car>>(data);
                try
                {

                    _context.Cars.AddRange(cars);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Something bad happened: {ex.Message}");
                    throw;
                }
                return null;
            }
            Console.WriteLine("SENT "+ _context.Cars.Count());
            return await _context.Cars.ToListAsync();

        }


        // List all cars that are available on a certain day.
        [HttpGet("free/{dateTime}")]
        public async Task<ActionResult<Car>> GetCarForDay(DateTime dateTime)
        {
            var car = await _context.Cars.Include(c=> c.Books).Where(car=>!car.Books.Any(b=>b.Date.Date.Equals(dateTime.Date))).ToListAsync();

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarExists(car.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
