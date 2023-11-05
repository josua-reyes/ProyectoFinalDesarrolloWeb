using CapaLogicaNegocio;
using CapaModelos;
using CapaModelos.DTO;
using CapaModelos.Modelos;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Xml;

namespace WebServicesSOAP
{
    /// <summary>
    /// Descripción breve de examenFinal
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]

    public class ProyectoFinal : System.Web.Services.WebService
    {
        ProyectoFinalBll _proyectoFinalBll;

        [WebMethod]
        public List<ProductoIndexDTO> ProductoIndex()
        {
            _proyectoFinalBll = new ProyectoFinalBll();
            return _proyectoFinalBll.ProductoIndex();
        }

        [WebMethod]
        public CamposGenericosRespuesta ProductoCreate(ProductoCreateDTO productoCreateDTO)
        {
            _proyectoFinalBll = new ProyectoFinalBll();
            return _proyectoFinalBll.ProductoCreate(productoCreateDTO);
        }

        [WebMethod]
        public CamposGenericosRespuesta IniciarProcesoProduccion(string codUsuarioProduccion)
        {
            _proyectoFinalBll = new ProyectoFinalBll();
            return _proyectoFinalBll.IniciarProcesoProduccion(codUsuarioProduccion);
        }
    }
}
