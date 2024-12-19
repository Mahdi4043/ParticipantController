using Microsoft.AspNetCore.Mvc;
using ParticipantsLib;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParticipantREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        public ParticipantsRepository _partiRepo;

        public ParticipantsController(ParticipantsRepository partiRepository)
        {
            _partiRepo = partiRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Participant>> Get()
        {

            var partis = _partiRepo.GetAll();
            if (partis != null && partis.Count() != 0)
            {
                return Ok(partis);
            }
            else
                return NotFound(partis);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Participant> Get(int id)
        {
            Participant parti = _partiRepo.GetById(id);
            if (parti != null)
            {
                return Ok(parti);
            }
            else return NotFound(parti);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult<Participant> Post([FromBody] Participant parti)
        {
            try
            {
                _partiRepo.AddParti(parti);
                return Created("Participant", parti);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Participant> Delete(int id)
        {
            Participant parti = _partiRepo.GetById(id);
            if (parti != null)
            {
                _partiRepo.Delete(parti.Id);
                return Ok(parti);
            }
            else
            {
                return NotFound();
            }

        }

    }
}
