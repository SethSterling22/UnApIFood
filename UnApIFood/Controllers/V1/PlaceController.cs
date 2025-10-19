using Microsoft.AspNetCore.Mvc;
using UnApIFood.Models;
using UnApIFood.Services;

namespace UnApIFood.Controllers.V1
{
    public class PlacesController : BaseController
    {

        private readonly PlacesService _placesService;

        public PlacesController(PlacesService placesService)
        {
            _placesService = placesService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlace(int id)
        {
            try
            {
                var place = await _placesService.GetPlace(id);

                if (place != null)
                {
                    return Ok(place);
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

        // GetALL
        [HttpGet]
        public async Task<ActionResult<List<Place>>> GetAll()
        {
            try
            {
                return Ok(await _placesService.GetAll());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Traer todos los lugares de la Universidad especificada!!!! :)
        [HttpGet("university/{UniversityId}")]
        public async Task<ActionResult<List<Place>>> GetAll(int UniversityId)
        {
            try
            {
                return Ok(await _placesService.GetAll(UniversityId));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Place>> CreatePlace(Place place)
        {
            try
            {
                if (place == null)
                {
                    return BadRequest("Se deben llenar todos los campos de la Universidad.");
                }

                var createdPlace = await _placesService.Post(place);
                return createdPlace;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Place>> UpdatePlace(int id, Place place)
        {
            try
            {
                if (id != place.Id)
                {
                    return BadRequest("El ID y el URL deben ser iguales");
                }

                var updatedPlace = await _placesService.Put(place);
                if (updatedPlace == null)
                {
                    return NotFound("No se encontró el Menú con ese Id.");
                }

                return Ok(updatedPlace);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeletePlace(int id)
        {
            try
            {
                await _placesService.DeletePlace(id);
                return Ok();
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("No se encontró Lugar con el Id dado"))
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