using CrazyMusicians.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using CrazyMusicians.Models;    

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private static List<Musicians> _musicians = new List<Musicians>()
        {
            new Musicians { Id = 1, Name = "Ahmet Çalgı", Profession = "Famous Instrument Player", FunnyFeatures = "Always plays the wrong notes, but very entertaining" },
            new Musicians { Id = 2, Name = "Zeynep Melodi", Profession = "Popular Melody Writer", FunnyFeatures = "Her songs are misunderstood, but very popular" },
            new Musicians { Id = 3, Name = "Cemil Akor", Profession = "Crazy Chordist", FunnyFeatures = "Frequently changes chords, but surprisingly talented" },
            new Musicians { Id = 4, Name = "Fatma Nota", Profession = "Surprise Note Producer", FunnyFeatures = "Always creates surprises while producing notes" },
            new Musicians { Id = 5, Name = "Hasan Ritim", Profession = "Rhythm Beast", FunnyFeatures = "Makes every rhythm in his own style, doesn't match, but it's funny" },
            new Musicians { Id = 6, Name = "Elif Armoni", Profession = "Harmony Master", FunnyFeatures = "Sometimes plays harmonies wrong, but is very creative" },
            new Musicians { Id = 7, Name = "Ali Perde", Profession = "Tone Applicator", FunnyFeatures = "Plays every note differently, always surprising" },
            new Musicians { Id = 8, Name = "Ayşe Rezonans", Profession = "Resonance Specialist", FunnyFeatures = "An expert on resonance, but sometimes makes too much noise" },
            new Musicians { Id = 9, Name = "Murat Ton", Profession = "Tone Enthusiast", FunnyFeatures = "His tonal differences are sometimes funny, but quite interesting" },
            new Musicians { Id = 10, Name = "Selin Akor", Profession = "Chord Wizard", FunnyFeatures = "Creates a magical atmosphere when she changes chords" }

        };

        // All routing operations were done according to week 5.

        // Calling the All list
        [HttpGet]
        public IEnumerable<Musicians> GetAll()
        {
            return _musicians;
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Musicians>> GetById([FromQuery] int id)
        {
            var musician = _musicians.Where(x => x.Id == id);
            return Ok(musician);
        }

        // Adding a new musician.
        [HttpPost]
        public ActionResult<Musicians> Create([FromBody] Musicians musicians)
        {
            musicians.Id = _musicians.Max(x => x.Id) + 1;
            _musicians.Add(musicians);
            return CreatedAtAction(nameof(GetById), new { id = musicians.Id }, musicians);
        }

        // Removing musician by Id.
        [HttpDelete("{id:int:min(1)}")]
        public IActionResult DeleteById(int id)
        {
            var musicianDel = _musicians.FirstOrDefault(x => x.Id == id);
            _musicians.Remove(musicianDel);
            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateMusician(int id, [FromBody] Musicians updateAll)
        {
            var musician = _musicians.FirstOrDefault(musicians => musicians.Id == id);
            if (musician == null)
            {
                return NoContent();
            }
            musician.Name = updateAll.Name;
            musician.Profession = updateAll.Profession;
            musician.FunnyFeatures = updateAll.FunnyFeatures;
            return NoContent();
        }
        //To updating the part you want.
        [HttpPatch("patch/{id:int:min(1)}")]
        public IActionResult PatchingMusician(int id, [FromBody] JsonPatchDocument<Musicians> patchDocument)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            if (musician == null)
            {
                return NoContent();
            }
            patchDocument.ApplyTo(musician);
            return NoContent();
        }

    }
    }

