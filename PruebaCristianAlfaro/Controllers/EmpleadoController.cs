using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaCristianAlfaro.Data;
using PruebaCristianAlfaro.Models;

namespace PruebaCristianAlfaro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private readonly AppDbContext appDbContext;
        private readonly ILogger<EmpleadoController> _logger;

        public EmpleadoController(AppDbContext appDbContext, ILogger<EmpleadoController> logger)
        {
            this.appDbContext = appDbContext;
            _logger = logger;
        }

        //Mostrar listado de empleado
        [HttpGet]
        public async Task<ActionResult<List<Empleado>>> GetEmpleados()
        {
            try
            {
                var empleados = await appDbContext.Empleados.ToListAsync();
                _logger.LogWarning("se esta listando los empleados");
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al obtener listado de empleados.");
                return StatusCode(500, new { message = "Ocurrió un error al obtener listado de empleados. Por favor, inténtelo más tarde." + ex.ToString() });
            }
        }

        //Agregar Empleado
        [HttpPost]
        public async Task<ActionResult<List<Empleado>>> AddEmpleados(Empleado nuevoEmpleado)
        {
            try
            {
                if (nuevoEmpleado != null)
                {
                    appDbContext.Empleados.Add(nuevoEmpleado);
                    await appDbContext.SaveChangesAsync();


                    var empleados = await appDbContext.Empleados.ToListAsync();
                    return Ok(empleados);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al obtener listado de empleados.");
                return StatusCode(500, new { message = "Ocurrió un error al agregar empleado. Por favor, inténtelo más tarde." + ex.ToString() });
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<Empleado>> UpdateEmpleado(Empleado actualizaEmpleado)
        {
            try
            {
                if (actualizaEmpleado != null)
                {
                    var employee = await appDbContext.Empleados.FirstOrDefaultAsync(e => e.Id == actualizaEmpleado.Id);
                    if (employee != null)
                    {
                        employee.Nombre = actualizaEmpleado.Nombre;
                        employee.Edad = actualizaEmpleado.Edad;
                        await appDbContext.SaveChangesAsync();

                        var employees = await appDbContext.Empleados.ToListAsync();
                        return Ok(employees);
                    }
                    return NotFound();
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al obtener listado de empleados.");
                return StatusCode(500, new { message = "Ocurrió un error al modificar el empleado. Por favor, inténtelo más tarde." + ex.ToString() });
            }
            return BadRequest();
        }

    }
}
