using CarrosData.Context;
using CarrosData.Models;
using CarrosServices.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarrosServices.Services
{
    public class CarrosServices : ICarrosRepository
    {
        public AppDbContext _context { get; }

        public CarrosServices(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<Carro>> GetCars()
        {
            var carsDB = await _context.Carros.ToListAsync();
            return carsDB;
        }

        public async Task<Carro> GetCarByID(int id)
        {
            var carDB = await _context.Carros.FirstOrDefaultAsync(x => x.id == id);

            return carDB;
        }

        public async Task<Carro> PostCarro(Carro car)
        {
            try
            {
                _context.Carros.Add(car);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return car;
        }

        public async Task<Carro> PutCarro(Carro car)
        {
            try
            {
                _context.Entry(car).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return null;
            }

            return car;
        }

        public async Task<Carro> PatchCarro(int id)
        {
            var carroDB = await _context.Carros.FirstOrDefaultAsync(x =>x.id == id);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return null;
            }

            return carroDB;
        }

        public async Task<bool> DelCarro(int id)
        {
            try
            {
                var carroDB = await _context.Carros.FirstOrDefaultAsync(x=>x.id == id);
                _context.Carros.Remove(carroDB);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

       
    }
}
