using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos.DTO
{
    public class ProductoCreateDTO
    {
        public int ID_PRODUCTO { get; set; }
        public string COD_PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal EXISTENCIA { get; set; }
        public decimal COSTO_REFERENCIA { get; set; }
        public decimal PRECIO { get; set; }
        public char ESTADO { get; set; }
        public string COD_PRODUCTO_CLASE { get; set; }
        public string COD_UNIDAD_MEDIDA { get; set; }
    }

    public class ProductoEditDTO
    {
        public int ID_PRODUCTO { get; set; }
        public string COD_PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal EXISTENCIA { get; set; }
        public decimal COSTO_REFERENCIA { get; set; }
        public decimal PRECIO { get; set; }
        public char ESTADO { get; set; }
        public string COD_PRODUCTO_CLASE { get; set; }
        public string COD_UNIDAD_MEDIDA { get; set; }
    }


}
