using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos.DTO
{
    public class ProductoIndexDTO
    {
        public string ID_PRODUCTO { get; set; }
        public string COD_PRODUCTO { get; set; }
        public string DESCRIPCION_PRODUCTO { get; set; }
        public decimal EXISTENCIA { get; set; }
        public string DESCRIPCION_CLASE_PRODUCTO { get; set; }
        public string DESCRIPCION_UNIDAD_MEDIDA { get; set; }
    }
}
