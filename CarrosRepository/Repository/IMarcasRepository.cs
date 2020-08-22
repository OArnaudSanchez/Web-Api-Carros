using CarrosData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarrosServices.Repository
{
    public interface IMarcasRepository
    {
        //Obtener las marcas
        Task<List<Marca>> GetMarcas();

        //Obtener una marca por su id
        Task<Marca> GetMarcaByID(int id);

        //Crear una nueva marca
        Task<Marca> PostMarca(Marca marca);

        //Editar una marca
        Task<Marca> PutMarca(Marca marca);

        //Actualizacion Parcial a una marca
        Task<Marca> PatchMarca(int id);

        //Eliminar una marca
        Task<bool> DelMarca(int id);
    }
}
