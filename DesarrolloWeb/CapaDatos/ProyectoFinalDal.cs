using CapaModelos;
using CapaModelos.DTO;
using CapaModelos.Modelos;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CapaDatos
{
    public class ProyectoFinalDal : ConexionBaseDatos
    {
        public ProyectoFinalDal()
        {

        }

        public DataTable EjecutarConsulta(string consultaSql)
        {
            DataTable resultadoConsultaDatos = new DataTable();
            resultadoConsultaDatos.TableName = "Datos";

            try
            {
                using (_Connection = new OracleConnection(_CadenaConexion))
                {
                    _Connection.Open();

                    using (OracleCommand command = new OracleCommand(consultaSql, _Connection))
                    {
                        command.CommandType = CommandType.Text;

                        using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                        {
                            adapter.Fill(resultadoConsultaDatos);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                _Connection.Close();
            }

            return resultadoConsultaDatos;
        }

        public CamposGenericosRespuesta EjecutarInsertUpdateDelete(string consultaSql)
        {
            CamposGenericosRespuesta camposGenericosRespuesta = new CamposGenericosRespuesta();
            camposGenericosRespuesta.Estado = "EXITO";

            try
            {
                using (_Connection = new OracleConnection(_CadenaConexion))
                {
                    _Connection.Open();

                    using (OracleCommand command = new OracleCommand(consultaSql, _Connection))
                    {
                       command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                camposGenericosRespuesta.Estado = "ERROR";
                camposGenericosRespuesta.DescripcionError = ex.ToString();
            }
            finally
            {
                _Connection.Close();
            }

            return camposGenericosRespuesta;
        }

        public List<ProductoIndexDTO> ProductoIndex()
        {
            List<ProductoIndexDTO> listaProductos = new List<ProductoIndexDTO>();

            string consultaSql = $@"
                    SELECT
                      P.ID_PRODUCTO,
                      P.COD_PRODUCTO,
                      P.DESCRIPCION AS DESCRIPCION_PRODUCTO,
                      P.EXISTENCIA,
                      PC.DESCRIPCION AS DESCRIPCION_CLASE_PRODUCTO,
                      UM.DESCRIPCION AS DESCRIPCION_UNIDAD_MEDIDA
                    FROM PRODUCTO P
                    JOIN PRODUCTO_CLASE PC
                      ON P.COD_PRODUCTO_CLASE = PC.COD_PRODUCTO_CLASE
                    JOIN UNIDAD_MEDIDA UM
                      ON P.COD_UNIDAD_MEDIDA = UM.COD_UNIDAD_MEDIDA
                    ORDER BY P.ID_PRODUCTO DESC";

            DataTable data = EjecutarConsulta(consultaSql);

            foreach (DataRow row in data.Rows)
            {
                ProductoIndexDTO productoDTO = new ProductoIndexDTO
                {
                    ID_PRODUCTO = row["ID_PRODUCTO"].ToString(),
                    COD_PRODUCTO = row["COD_PRODUCTO"].ToString(),
                    DESCRIPCION_PRODUCTO = row["DESCRIPCION_PRODUCTO"].ToString(),
                    EXISTENCIA = Convert.ToDecimal(row["EXISTENCIA"]),
                    DESCRIPCION_CLASE_PRODUCTO = row["DESCRIPCION_CLASE_PRODUCTO"].ToString(),
                    DESCRIPCION_UNIDAD_MEDIDA = row["DESCRIPCION_UNIDAD_MEDIDA"].ToString()
                };

                listaProductos.Add(productoDTO);
            }

            return listaProductos;
        }

        public CamposGenericosRespuesta ProductoCreate(ProductoCreateDTO productoCreateDTO)
        {
            string consultaSql = $@"
                    INSERT INTO PRODUCTO (COD_PRODUCTO,
                     DESCRIPCION,
                     EXISTENCIA,
                     COSTO_REFERENCIA,
                     PRECIO,
                     ESTADO,
                     COD_PRODUCTO_CLASE,
                     COD_UNIDAD_MEDIDA)
                      VALUES ('{productoCreateDTO.COD_PRODUCTO}',
                       '{productoCreateDTO.DESCRIPCION}',
                       CAST({productoCreateDTO.EXISTENCIA} AS Number(18,
                       4)),
                       CAST({productoCreateDTO.COSTO_REFERENCIA} AS Number(18,
                       2)),
                       CAST({productoCreateDTO.PRECIO} AS Number(18,
                       2)),
                       '{productoCreateDTO.ESTADO}',
                       '{productoCreateDTO.COD_PRODUCTO_CLASE}',
                       '{productoCreateDTO.COD_UNIDAD_MEDIDA}')";

            return EjecutarInsertUpdateDelete(consultaSql);
        }


        public CamposGenericosRespuesta IniciarProcesoProduccion(string codUsuarioProduccion)
        {
            CamposGenericosRespuesta camposGenericosRespuesta = new CamposGenericosRespuesta();

            try
            {
                using (_Connection = new OracleConnection(_CadenaConexion))
                {
                    _Connection.Open();
                    try
                    {
                        _Transaction = _Connection.BeginTransaction();

                        using (_Command = new OracleCommand("PKG_PANADERIA_SAN_CARLOS.INICIAR_PROCESO_PRODUCCION", _Connection))
                        {
                            _Command.Transaction = _Transaction;
                            _Command.CommandType = CommandType.StoredProcedure;

                            _Command.Parameters.Add("COD_USUARIO_PRODUCCION", OracleDbType.Varchar2, 50).Value = codUsuarioProduccion;
                            _Command.Parameters.Add("pError", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                            _Command.Parameters.Add("pDescripcionError", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                            _Command.ExecuteNonQuery();

                            camposGenericosRespuesta.Estado = _Command.Parameters["pError"].Value.ToString();
                            camposGenericosRespuesta.DescripcionError = _Command.Parameters["pDescripcionError"].Value.ToString();

                            if (camposGenericosRespuesta.Estado.Contains("-1"))
                            {
                                throw new Exception(camposGenericosRespuesta.DescripcionError);
                            }
                        }
                        _Transaction.Commit();
                        camposGenericosRespuesta.Estado = "EXITO";
                    }
                    catch (Exception ex)
                    {
                        _Transaction.Rollback();
                        camposGenericosRespuesta.Estado = "ERROR";
                        camposGenericosRespuesta.DescripcionError = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                camposGenericosRespuesta.Estado = "ERROR";
                camposGenericosRespuesta.DescripcionError = ex.Message;

            }
            finally
            {
                _Connection.Close();
            }
            return camposGenericosRespuesta;
        }
    }
}
