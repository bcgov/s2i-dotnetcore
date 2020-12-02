/*
 * REST API Documentation for the MOTI School Bus Application
 *
 * The School Bus application tracks that inspections are performed in a timely fashion. For each school bus the application tracks information about the bus (including data from ICBC, NSC, etc.), it's past and next inspection dates and results, contacts, and the inspector responsible for next inspecting the bus.
 *
 * OpenAPI spec version: v1
 * 
 * 
 */

using Microsoft.AspNetCore.Mvc;
using SchoolBusAPI.Models;
using SchoolBusAPI.Services;
using SchoolBusAPI.Authorization;

namespace SchoolBusAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _service;

        /// <summary>
        /// Create a controller and set the service
        /// </summary>
        public DistrictController(IDistrictService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/districts")]
        [RequiresPermission(Permissions.CodeRead)]
        public virtual IActionResult DistrictsGet()
        {
            return this._service.DistrictsGetAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of District to delete</param>
        /// <response code="200">OK</response>
        /// <response code="404">District not found</response>
        [HttpPost]
        [Route("/api/districts/{id}/delete")]
        [RequiresPermission(Permissions.CodeWrite)]
        public virtual IActionResult DistrictsIdDeletePost([FromRoute]int id)
        {
            return this._service.DistrictsIdDeletePostAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of District to fetch</param>
        /// <response code="200">OK</response>
        /// <response code="404">District not found</response>
        [HttpGet]
        [Route("/api/districts/{id}")]
        [RequiresPermission(Permissions.CodeRead)]
        public virtual IActionResult DistrictsIdGet([FromRoute]int id)
        {
            return this._service.DistrictsIdGetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of District to fetch</param>
        /// <param name="item"></param>
        /// <response code="200">OK</response>
        /// <response code="404">District not found</response>
        [HttpPut]
        [Route("/api/districts/{id}")]
        [RequiresPermission(Permissions.CodeWrite)]
        public virtual IActionResult DistrictsIdPut([FromRoute]int id, [FromBody]District item)
        {
            return this._service.DistrictsIdPutAsync(id, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Returns the Service Areas for a specific region</remarks>
        /// <param name="id">id of District for which to fetch the ServiceAreas</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/districts/{id}/serviceareas")]
        [RequiresPermission(Permissions.CodeRead)]
        public virtual IActionResult DistrictsIdServiceareasGet([FromRoute]int id)
        {
            return this._service.DistrictsIdServiceareasGetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <response code="201">District created</response>
        [HttpPost]
        [Route("/api/districts")]
        [RequiresPermission(Permissions.CodeWrite)]
        public virtual IActionResult DistrictsPost([FromBody]District item)
        {
            return this._service.DistrictsPostAsync(item);
        }
    }
}
