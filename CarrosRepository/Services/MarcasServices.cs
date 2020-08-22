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
    public class MarcasServices : IMarcasRepository
    {
        public AppDbContext _context { get; }

        public MarcasServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Marca>> GetMarcas()
        {
            var marcasDB = await _context.Marcas.ToListAsync();
            return marcasDB;
        }

        public async Task<Marca> GetMarcaByID(int id)
        {
            var marcaDB = await _context.Marcas.Include(x => x.Carros).FirstOrDefaultAsync(x=>x.id == id);
            return marcaDB;
        }

        public async Task<Marca> PostMarca(Marca marca)
        {
            try
            {
                _context.Marcas.Add(marca);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return marca;
        }

        public async Task<Marca> PutMarca(Marca marca)
        {
            try
            {
                _context.Entry(marca).State = EntityState.Modified;
               await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return marca;
        }

        public async Task<Marca> PatchMarca(int id)
        {
            var marcaPatch = await _context.Marcas.FirstOrDefaultAsync(x=>x.id == id);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return null;
            }

            return marcaPatch;
        }

        public async Task<bool> DelMarca(int id)
        {
            try
            {
                var marcaDb = await _context.Marcas.FirstOrDefaultAsync(x=>x.id == id);
                _context.Remove(marcaDb);
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
