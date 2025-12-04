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
    public class RazasController : ControllerBase
    {
        private readonly ZoologicoApiContext _context;

        public RazasController(ZoologicoApiContext context)
        {
            _context = context;
        }

        // GET: api/Razas
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Raza>>>> GetRaza()
        {
            try
            {
                var data = await _context.Raza.ToListAsync();
                return ApiResult<List<Raza>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Raza>>.Fail(ex.Message);
            }
        }

        // GET: api/Razas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Raza>>> GetRaza(int id)
        {
            try
            {
                                var raza = await _context
                    .Raza
                    .Include(r => r.Animales)
                    .FirstOrDefaultAsync(r=> r.Codigo == id);
                if (raza == null)
                {
                   return ApiResult<Raza>.Fail("Raza no encontrada");
                }
                return ApiResult<Raza>.Ok(raza);
            }
            catch (Exception ex)
            {
                return ApiResult<Raza>.Fail(ex.Message);
            }
        }

        // PUT: api/Razas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Raza>>> PutRaza(int id, Raza raza)
        {
            if (id != raza.Codigo)
            {
                return ApiResult<Raza>.Fail("El ID de la raza no coincide");
            }

            _context.Entry(raza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RazaExists(id))
                {
                    return ApiResult<Raza>.Fail("Raza no encontrada");
                }
                else
                {
                    return ApiResult<Raza>.Fail(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Razas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Raza>>> PostRaza(Raza raza)
        {
            try { 
                _context.Raza.Add(raza);
                await  _context.SaveChangesAsync();
                return ApiResult<Raza>.Ok(raza);
            }
            catch (Exception ex)
            {
                return ApiResult<Raza>.Fail(ex.Message);
            }
    
        }

        // DELETE: api/Razas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Raza>>> DeleteRaza(int id)
        {
            try
            {
                var raza = await _context.Raza.FindAsync(id);
                if (raza == null)
                {
                    return ApiResult<Raza>.Fail("Raza no encontrada");
                }
                _context.Raza.Remove(raza);
                await _context.SaveChangesAsync();
                return ApiResult<Raza>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Raza>.Fail(ex.Message);
            }
           
        }

        private bool RazaExists(int id)
        {
            return _context.Raza.Any(e => e.Codigo == id);
        }
    }
}
