using Microsoft.AspNetCore.Hosting;
using Portail_e_book.Models.DAL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.BLL
{
    public class BLL_Document
    {
        public static IEnumerable<Document> getAllDocument()
        {
            return DAL_Document.getAllDocument();
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
   
        public static Document getDocumentBy(string Champ, string Valeur)
        {
            return DAL_Document.getDocumentBy(Champ, Valeur);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/

        public static IEnumerable<Document> getAllDocumentBy(string Field, string Value)
        {
            return DAL_Document.getAllDocumentBy(Field, Value);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
 
        public static JsonResponse AddDocument(Document d)
        {
            return DAL_Document.AddDocument(d);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/

        public static JsonResponse UpdateDocument(Document d)
        {
            return DAL_Document.UpdateDocument(d);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/

        public static JsonResponse DeleteDocument(int? Id)
        {
            /*----------------------------------*/
            List<DocumentAuthor> listDocumentAuthor = DAL_DocumentAuthor.getAllDocumentAuthorBy("IdDocument", Id.ToString());
            for (int i = 0; i < listDocumentAuthor.Count; i++)
            {
                BLL_DocumentAuthor.DeleteDocumentAuthor(listDocumentAuthor[i].IdDocumentAuthor);
            }

            List<DocumentFiles> listDocumentFiles = DAL_DocumentFiles.getAllDocumentFilesBy("IdDocument", Id.ToString());
            for (int i = 0; i < listDocumentFiles.Count; i++)
            {
                BLL_DocumentFiles.DeleteDocumentFiles(listDocumentFiles[i].IdDocumentFiles);
            }

            List<DocumentCatalogue> listDocumentCatalogue = DAL_DocumentCatalogue.getAllDocumentCatalogueBy("IdDocument", Id.ToString());
            for (int i = 0; i < listDocumentCatalogue.Count; i++)
            {
                BLL_DocumentCatalogue.DeleteDocumentCatalogue(listDocumentCatalogue[i].IdDocumentCatalogue);
            }
            /*------------------------------*/
            return DAL_Document.DeleteDocumentBy("Id", Id.ToString());
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Create or Update Document
        public static Document Upsert(int? Id)
        {
            if (Id == null)
            {
                //create
                return new Document();
            }
            else
            {
                //Update
                return getDocumentBy("Id", Id.ToString());
            }
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/

        #region Copy File to Server and Delete File from Server
    
        private static void CopyFilesToServer(Document document, IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                string uniqueFileName = document.Fichier != null ? document.Fichier.FileName.ToString() : "";
                
                if (document.Fichier != null)
                {
                    uniqueFileName = Path.GetFileNameWithoutExtension(Path.GetFileName(uniqueFileName)) + "_" + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(uniqueFileName);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    document.Fichier.CopyTo(new FileStream(filePath, FileMode.Create));
                }
            }
            catch { }
        }
      

        private static void DeleteFilesFromServer(Document document, IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                string uniqueFileName = document.FileName;
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, uniqueFileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch { }
        }
        #endregion
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        #region API Calls
        public static JsonResponse UpsertApi(Document document,  IWebHostEnvironment hostingEnvironment)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            if (document.Id == 0)
            {
                //create
                jr = AddDocument(document);
                if (jr.success)
                {
                    CopyFilesToServer(document, hostingEnvironment);
                }
            }
            else
            {
                //update
                jr = UpdateDocument(document);
                if (jr.success)
                {
                    CopyFilesToServer(document, hostingEnvironment);
                }
            }
            return jr;
        }
       
        public static JsonResponse DeleteApi(int Id, IWebHostEnvironment hostingEnvironment)
        {
            JsonResponse jr = new JsonResponse();
            var documentFromDb = getDocumentBy("Id", Id.ToString());
            if (documentFromDb == null)
            {
                jr.success = false;
                jr.message = "Error while Deleting";
                return jr;
            }
            else
            {
                jr = DeleteDocument(Id);
                if (jr.message == "1")
                {
                    DeleteFilesFromServer(documentFromDb, hostingEnvironment);
                    jr.success = true;
                    jr.message = "Delete successful";
                }
                else
                {
                    jr.success = false;
                    jr.message = jr.message;
                }
            }
            return jr;
        }
        #endregion


    }
}
