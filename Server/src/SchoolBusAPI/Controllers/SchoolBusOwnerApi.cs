/*
 * REST API Documentation for the MOTI School Bus Application
 *
 * The School Bus application tracks that inspections are performed in a timely fashion. For each school bus the application tracks information about the bus (including data from ICBC, NSC, etc.), it's past and next inspection dates and results, contacts, and the inspector responsible for next inspecting the bus.
 *
 * OpenAPI spec version: v1
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.SwaggerGen.Annotations;
using SchoolBusAPI.Models;
using SchoolBusAPI.ViewModels;
using SchoolBusAPI.Services;

namespace SchoolBusAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SchoolBusOwnerApiController : Controller
    {
        private readonly ISchoolBusOwnerApiService _service;

        /// <summary>
        /// Create a controller and set the service
        /// </summary>
        public SchoolBusOwnerApiController(ISchoolBusOwnerApiService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Returns list of available FavouriteContextTypes</remarks>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/favouritecontexttypes")]
        [SwaggerOperation("FavouritecontexttypesGet")]
        [SwaggerResponse(200, type: typeof(List<FavouriteContextType>))]
        public virtual IActionResult FavouritecontexttypesGet()
        {
            return this._service.FavouritecontexttypesGetAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <response code="201">SchoolBusOwners created</response>
        [HttpPost]
        [Route("/api/schoolbusowners/bulk")]
        [SwaggerOperation("SchoolbusownersBulkPost")]
        public virtual IActionResult SchoolbusownersBulkPost([FromBody]SchoolBusOwner[] items)
        {
            return this._service.SchoolbusownersBulkPostAsync(items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/schoolbusowners")]
        [SwaggerOperation("SchoolbusownersGet")]
        [SwaggerResponse(200, type: typeof(List<SchoolBusOwner>))]
        public virtual IActionResult SchoolbusownersGet()
        {
            return this._service.SchoolbusownersGetAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Returns attachments for a particular SchoolBusOwner</remarks>
        /// <param name="id">id of SchoolBusOwner to fetch attachments for</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/schoolbusowners/{id}/attachments")]
        [SwaggerOperation("SchoolbusownersIdAttachmentsGet")]
        [SwaggerResponse(200, type: typeof(List<SchoolBusOwnerAttachment>))]
        public virtual IActionResult SchoolbusownersIdAttachmentsGet([FromRoute]int id)
        {
            return this._service.SchoolbusownersIdAttachmentsGetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of SchoolBusOwner to delete</param>
        /// <response code="200">OK</response>
        /// <response code="404">SchoolBusOwner not found</response>
        [HttpPost]
        [Route("/api/schoolbusowners/{id}/delete")]
        [SwaggerOperation("SchoolbusownersIdDeletePost")]
        public virtual IActionResult SchoolbusownersIdDeletePost([FromRoute]int id)
        {
            return this._service.SchoolbusownersIdDeletePostAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Returns a particular SchoolBusOwner</remarks>
        /// <param name="id">id of SchoolBusOwner to fetch</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/schoolbusowners/{id}")]
        [SwaggerOperation("SchoolbusownersIdGet")]
        [SwaggerResponse(200, type: typeof(List<SchoolBusOwner>))]
        public virtual IActionResult SchoolbusownersIdGet([FromRoute]int id)
        {
            return this._service.SchoolbusownersIdGetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Returns notes for a particular SchoolBusOwner</remarks>
        /// <param name="id">id of SchoolBusOwner to fetch notes for</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/schoolbusowners/{id}/notes")]
        [SwaggerOperation("SchoolbusownersIdNotesGet")]
        [SwaggerResponse(200, type: typeof(List<SchoolBusOwnerNote>))]
        public virtual IActionResult SchoolbusownersIdNotesGet([FromRoute]int id)
        {
            return this._service.SchoolbusownersIdNotesGetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of SchoolBusOwner to fetch</param>
        /// <param name="item"></param>
        /// <response code="200">OK</response>
        /// <response code="404">SchoolBusOwner not found</response>
        [HttpPut]
        [Route("/api/schoolbusowners/{id}")]
        [SwaggerOperation("SchoolbusownersIdPut")]
        [SwaggerResponse(200, type: typeof(SchoolBusOwner))]
        public virtual IActionResult SchoolbusownersIdPut([FromRoute]int id, [FromBody]SchoolBusOwner item)
        {
            return this._service.SchoolbusownersIdPutAsync(id, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <response code="201">SchoolBusOwner created</response>
        [HttpPost]
        [Route("/api/schoolbusowners")]
        [SwaggerOperation("SchoolbusownersPost")]
        [SwaggerResponse(200, type: typeof(SchoolBusOwner))]
        public virtual IActionResult SchoolbusownersPost([FromBody]SchoolBusOwner item)
        {
            return this._service.SchoolbusownersPostAsync(item);
        }
    }
}
