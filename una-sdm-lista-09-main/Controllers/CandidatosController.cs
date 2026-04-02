using Microsoft.AspNetCore.Mvc;
using EleicaoBrasilApi.Data;
using EleicaoBrasilApi.Models;

namespace EleicaoBrasilApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CandidatosController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Candidatos.ToList());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var candidato = _context.Candidatos.Find(id);

            if (candidato == null)
                return NotFound();

            return Ok(candidato);
        }

        // POST (COM VALIDAÇÃO)
        [HttpPost]
        public IActionResult Post(Candidato novoCandidato)
        {
            // VALIDAÇÃO: número não pode repetir
            if (_context.Candidatos.Any(c => c.Numero == novoCandidato.Numero))
            {
                return BadRequest("Número já cadastrado");
            }

            _context.Candidatos.Add(novoCandidato);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = novoCandidato.Id }, novoCandidato);
        }

        // PUT (ATUALIZA COM ViceNome)
        [HttpPut("{id}")]
        public IActionResult Put(int id, Candidato candidatoAtualizado)
        {
            var candidato = _context.Candidatos.Find(id);

            if (candidato == null)
                return NotFound();

            candidato.Nome = candidatoAtualizado.Nome;
            candidato.Numero = candidatoAtualizado.Numero;
            candidato.Partido = candidatoAtualizado.Partido;
            candidato.ViceNome = candidatoAtualizado.ViceNome;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var candidato = _context.Candidatos.Find(id);

            if (candidato == null)
                return NotFound();

            _context.Candidatos.Remove(candidato);
            _context.SaveChanges();

            return NoContent();
        }

        // FILTRO POR PARTIDO
        [HttpGet("partido/{nomeDoPartido}")]
        public IActionResult GetByPartido(string nomeDoPartido)
        {
            var candidatos = _context.Candidatos
                .Where(c => c.Partido == nomeDoPartido)
                .ToList();

            return Ok(candidatos);
        }
    }
}