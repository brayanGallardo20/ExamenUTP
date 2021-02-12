using CCommon;
using CDatos;
using CEntidad.Request;
using CEntidad.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CServicio
{
    public class UniversidadService
    {

        private readonly DB_UTPEntities _Context;
        private ResponseBase<TBL_NOTA> _Response;

        public UniversidadService() {
            _Context = new DB_UTPEntities();
        }

        public ResponseBase<List<NotaResponse>> listarNotas(int? idAlumno)
        {
            ResponseBase<List<NotaResponse>> listResponse = new ResponseBase<List<NotaResponse>>();
            List<NotaResponse> notaResponse = new List<NotaResponse>();
            try
            {

                var queryUsuario = _Context.TBL_ALUMNO.Where(x => x.INT_ALUMNOID == idAlumno).Select(x => x.INT_ALUMNOID).FirstOrDefault();

                if (queryUsuario != 0)
                {

                    var queryResult = _Context.TBL_NOTA.
                        Join(_Context.TBL_CURSO, x => x.INT_CURSO_ID, y => y.INT_CURSOID, (x, y) => new { x, y }).
                        Join(_Context.TBL_ALUMNO, z => z.x.INT_ALUMNOID, zo => zo.INT_ALUMNOID, (z, zo) => new { z, zo }).
                        Where(a => (a.z.x.INT_ALUMNOID == idAlumno)
                        ).OrderBy(x => x.z.x.INT_ALUMNOID).
                        Select(b => new NotaResponse
                        {
                            VCH_ALUMNO = b.zo.VCH_NOMBRE + " " + b.zo.VCH_APELLIDO_PAT + " " + b.zo.VCH_APELLIDO_MAT,
                            VCH_CURSO = b.z.y.VCH_NOMBRE,
                            DEC_NOTA = b.z.x.DEC_NOTA
                        });

                    if (queryResult.Count() > 0)
                    {
                        notaResponse = queryResult.ToList();
                        listResponse = new UtilityResponse<List<NotaResponse>>().SetResponseBaseForObj(notaResponse);
                    }
                    else
                    {
                        listResponse = new UtilityResponse<List<NotaResponse>>().SetResponseBaseForValidationNoteString(notaResponse);
                    }

                }
                else
                {
                    
                 listResponse = new UtilityResponse<List<NotaResponse>>().SetResponseBaseForValidationAlumnString(notaResponse);
                }
            }
            catch (Exception ex)
            {
                listResponse = new UtilityResponse<List<NotaResponse>>().SetResponseBaseForException(ex);
            }
            return listResponse;
        }

        public ResponseBase<TBL_NOTA> crearNotas(NotaRequest request)
        {
            TBL_NOTA tblNota = new TBL_NOTA();

            try
            {
                var queryUsuario = _Context.TBL_ALUMNO.Where(x => x.INT_ALUMNOID == request.INT_ALUMNOID).Select(x => x.INT_ALUMNOID).FirstOrDefault();

                if (queryUsuario != 0)
                {
                    var queryCurso = _Context.TBL_CURSO.Where(x => x.INT_CURSOID == request.INT_CURSO_ID).Select(x => x.INT_CURSOID).FirstOrDefault();

                    if(queryCurso != 0)
                    {

                        tblNota.INT_CURSO_ID = request.INT_CURSO_ID;
                        tblNota.INT_ALUMNOID = request.INT_ALUMNOID;
                        tblNota.DEC_NOTA = request.DEC_NOTA;
                        tblNota.BIT_ESTADO = true;
                        tblNota.DAT_FECHA_CREA = DateTime.Now;
                        tblNota.INT_USU_CREA = 1;

                        _Context.TBL_NOTA.Add(tblNota);
                        _Context.SaveChanges();

                        _Response = new UtilityResponse<TBL_NOTA>().SetResponseBaseForObj(tblNota);

                    }
                    else
                    {
                        _Response = new UtilityResponse<TBL_NOTA>().SetResponseBaseForValidationCourseString(tblNota);
                    }
                }
                else
                {
                    _Response = new UtilityResponse<TBL_NOTA>().SetResponseBaseForValidationAlumnString(tblNota);
                }

            }
            catch (Exception ex)
            {
                _Response = new UtilityResponse<TBL_NOTA>().SetResponseBaseForException(ex);
            }
            finally
            {
                _Context.Database.Connection.Close();
            }
            return _Response;
        }

    }
}
