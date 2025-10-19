using Microsoft.AspNetCore.Mvc;
using UnApIFood.Models;
using UnApIFood.Services;

namespace UnApIFood.Controllers.V1
{
    public class UniversitiesController : BaseController
    {


        private readonly UniversitiesService _universitiesService;

        public UniversitiesController(UniversitiesService universitiesService)
        {
            _universitiesService = universitiesService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUniversity(int id)
        {
            try
            {
                var user = await _universitiesService.GetUniversity(id);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<University>>> GetAll()
        {
            try
            {
                return Ok(await _universitiesService.GetAll());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<University>> CreateUniversity(University university)
        {
            try
            {
                if (university == null)
                {
                    return BadRequest("Se deben llenar todos los campos de la Universidad.");
                }

                var createdUniversity = await _universitiesService.Post(university);
                return createdUniversity;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<University>> UpdateUniversity(int id, University university)
        {
            try
            {
                if (id != university.Id)
                {
                    return BadRequest("El ID y el URL deben ser iguales");
                }

                var updatedUniversity = await _universitiesService.Put(university);
                if (updatedUniversity == null)
                {
                    return NotFound("No se encontró Universidad con el Id dado.");
                }

                return Ok(updatedUniversity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            try
            {
                await _universitiesService.DeleteUniversity(id);
                return Ok();
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("No se encontró Universidad con el Id dado"))
                {
                    // El usuario con el ID especificado no existe
                    return NotFound();
                }
                else
                {
                    // Otro tipo de error
                    return StatusCode(500, ex.Message);
                }
            }
        }

    }
}