using CCommon;
using CDatos;
using CEntidad.Request;
using CEntidad.Response;
using CServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace ExamenUTP.Controllers
{
    [Authorize]
    public class UniversidadController : ApiController
    {

        private readonly UniversidadService _universidadService;

        public UniversidadController()
        {
            _universidadService = new UniversidadService();
        }

        [HttpGet]
        public JsonResult<ResponseBase<List<NotaResponse>>>  listarNotas(int idAlumno)
        {
            return Json(_universidadService.listarNotas(idAlumno));
        }

        [HttpPost]
        public JsonResult<ResponseBase<TBL_NOTA>> crearNota(NotaRequest request)
        {
            return Json(_universidadService.crearNotas(request));
        }

    }
}
