using BlazorCRUD.Server.Data;
using BlazorCRUD.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BlazorCRUD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SuperHeroController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            var heroes = await _context.SuperHeroes.Include(u => u.Comic).ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            var comics = await _context.Comics.ToListAsync();
            return Ok(comics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHero(int? id)
        {
            var hero = await _context.SuperHeroes
                .Include(u => u.Comic)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (hero == null)
            {
                return NotFound("Sorry No Hero");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateHero(SuperHero hero)
        {
            hero.Comic = null;

            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await GetDBHeroes());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero, int id)
        {
            var dbhero = await _context.SuperHeroes
                 .Include(u => u.Comic)
                 .FirstOrDefaultAsync(u => u.Id == id);

            if (dbhero == null)
            {
                return NotFound("Sorry No Hero");
            }
            dbhero.FirstName = hero.FirstName;
            dbhero.LastName = hero.LastName;
            dbhero.HeroName = hero.HeroName;
            dbhero.ComicId = hero.ComicId;

            await _context.SaveChangesAsync();

            return Ok(await GetDBHeroes());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes
               .Include(u => u.Comic)
               .FirstOrDefaultAsync(u => u.Id == id);

            if (hero == null)
            {
                return NotFound("Sorry No Hero");
            }

            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(await GetDBHeroes());
        }

        private async Task<List<SuperHero>> GetDBHeroes()
        {
            return await _context.SuperHeroes.Include(u => u.Comic).ToListAsync();
        }

    }
}
