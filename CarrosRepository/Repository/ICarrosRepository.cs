using CarrosData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarrosServices.Repository
{
    public interface ICarrosRepository
    {
        //Obtener todos los carros
        Task<List<Carro>> GetCars();

        //Obtener un carro por su id
        Task<Carro> GetCarByID(int id);

        //Crear un nuevo carro
        Task<Carro> PostCarro(Carro carro);

        //Actualizar un carro
        Task<Carro> PutCarro(Carro carro);

        //Actualizacion parcial de un carro
        Task<Carro> PatchCarro(int id);

        //Eliminar un carro
        Task<bool> DelCarro(int id);
    }
}
