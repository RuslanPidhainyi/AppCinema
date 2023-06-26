using AppCinema.Data.Base;
using AppCinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>,  IActorsService
    {
        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context) : base(context) { }



        //To juz nam nie potrzebno tak jak wykorzystamy  base(context).
        /*
        //powączenia do naszych BD 
        public ActorsService(AppDbContext context)
        {
            _context = context;
        }
        */

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //To juz nam nie potrzebno tak jak wykorzystamy EntityBaseRepository.
        /*
        //GetAllAsync
        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            var result = await _context.Actors.ToListAsync();
            return result;
        }
        

        //AddAsync
        public async Task AddAsync (Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }


        //GetByIdAsync
        public async Task<Actor> GetByIdAsync(int id)
        {
            //otrymujemy aktora
            var result = await _context.Actors.FirstOrDefaultAsync(e => e.Id == id);
           return result;
        }


        //UpdateAsync
        public async Task<Actor> UpdateAsync(int id, Actor newActor)
        {
            _context.Update(newActor);
            await _context.SaveChangesAsync();
            return newActor;
        }


        //DeleteAsync
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(e => e.Id == id);
             _context.Remove(result);
            await _context.SaveChangesAsync();
        }
        */


    }
}
