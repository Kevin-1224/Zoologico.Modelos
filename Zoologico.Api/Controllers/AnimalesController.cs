using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoologico.Modelos;

namespace Zoologico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalesController : ControllerBase
    {
        private readonly ZoologicoApiContext _context;

        public AnimalesController(ZoologicoApiContext context)
        {
            _context = context;
        }

        // GET: api/Animales
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Animal>>>> GetAnimal()
        {
            try
            {
                var data = await _context.Animal.ToListAsync();
                return ApiResult<List<Animal>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Animal>>.Fail(ex.Message);
            }
        }

        // GET: api/Animales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Animal>>> GetAnimal(int id)
        {
            try
            {
                var animal = await _context.Animal
                    .Include(a => a.Especie)
                    .Include(a => a.Raza).FirstOrDefaultAsync(a => a.Id == id);
                if (animal == null)
                {
                    return ApiResult<Animal>.Fail("Animal no encontrado");
                }
                return ApiResult<Animal>.Ok(animal);
            }
            catch (Exception ex)
            {
                return ApiResult<Animal>.Fail(ex.Message);
            }
        }

        // PUT: api/Animales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Animal>>> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return ApiResult<Animal>.Fail("El ID del animal no coincide");
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!AnimalExists(id))
                {
                    return ApiResult<Animal>.Fail("Animal no encontrado");
                }
                else
                {
                    return ApiResult<Animal>.Fail(ex.Message);
                }
            }

            return ApiResult<Animal>.Ok(animal);
        }

        // POST: api/Animales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Animal>>> PostAnimal(Animal animal)
        {
            try
            {
                _context.Animal.Add(animal);
                await _context.SaveChangesAsync();
                return ApiResult<Animal>.Ok(animal);
            }
            catch (Exception ex)
            {
                return ApiResult<Animal>.Fail(ex.Message);
            }



        }

        // DELETE: api/Animales/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Animal>>> DeleteAnimal(int id)
        {
            try
            {
                var animal = await _context.Animal.FindAsync(id);
                if (animal == null)
                {
                    return ApiResult<Animal>.Fail("Animal no encontrado");
                }
                _context.Animal.Remove(animal);
                await _context.SaveChangesAsync();
                return ApiResult<Animal>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Animal>.Fail(ex.Message);
            }
        }



        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.Id == id);
        }
      
    }
}
