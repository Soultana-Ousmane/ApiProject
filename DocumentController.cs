using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Portail_e_book.Models.BLL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Controllers
{
    [Route("api/Document")]
    [ApiController]
    public class DocumentController : Controller
    { 
        private readonly IWebHostEnvironment hostingEnvironment;
        public DocumentController(IWebHostEnvironment environment)
        {
            hostingEnvironment = environment;
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        #region API Calls

        [HttpGet]
        [Route("getAllDocument")]
        public IActionResult getAll()
        {
            return Json(new { Data = BLL_Document.getAllDocument() });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getDocumentBy")]
        public IActionResult getBy(string Field, string Value)
        {
            return Json(new { Data = BLL_Document.getDocumentBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getAllDocumentBy")]
        public IActionResult getAllBy(string Field, string Value)
        {
            return Json(new { Data = BLL_Document.getAllDocumentBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpPost]
        [Route("UpsertDocument")]
        public IActionResult Upsert(Document d)
        {
           
            return Json(BLL_Document.UpsertApi(d, hostingEnvironment));
            
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpDelete]
        [Route("DeleteDocument")]
        public IActionResult Delete(int id)
        {
            return Json(BLL_Document.DeleteApi(id, hostingEnvironment)); 
        }
        #endregion
    }
}
