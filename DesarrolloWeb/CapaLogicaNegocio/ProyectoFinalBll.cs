using CapaDatos;
using CapaModelos;
using CapaModelos.DTO;
using CapaModelos.Modelos;
using System.Collections.Generic;
using System.Data;

namespace CapaLogicaNegocio
{
    public class ProyectoFinalBll
    {
        ProyectoFinalDal _proyectoFinalDal;
        public ProyectoFinalBll()
        {
            _proyectoFinalDal = new ProyectoFinalDal();
        }

        public List<ProductoIndexDTO> ProductoIndex()
        {
            return _proyectoFinalDal.ProductoIndex();
        }

        public CamposGenericosRespuesta ProductoCreate(ProductoCreateDTO productoCreateDTO)
        {
            return _proyectoFinalDal.ProductoCreate(productoCreateDTO);
        }

        public CamposGenericosRespuesta IniciarProcesoProduccion(string codUsuarioProduccion)
        {
            return _proyectoFinalDal.IniciarProcesoProduccion(codUsuarioProduccion);
        }
    }
}
