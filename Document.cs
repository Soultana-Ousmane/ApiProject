using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.Entity
{
    public class Document
    {
        [Required]
        public int Id { get; set; }
        public int IdCollection { get; set; }
        public string Editor { get; set; }
        [Required]
        public string Doi { get; set; }


        public string OriginalTitle { get; set; }
        public string TitlesVariants { get; set; }
        public string Subtitle { get; set; }
        public string Foreword { get; set; }
        public string Keywords { get; set; }
        public string FileName { get; set; }// le nom du fichier
        public IFormFile Fichier { get; set; } // le fichier


        public string FileFormat { get; set; }// le format du fichier
        public string CoverPage { get; set; }
        public string Url { get; set; }
        public string DocumentType { get; set; }
        public string OriginalLanguage { get; set; }
        public string LanguagesVariants { get; set; }
        public string Translator { get; set; }
        public string AccessType { get; set; }
        public string State { get; set; }


        public Decimal Price { get; set; }
        public Decimal SellingPrice { get; set; }
        public Decimal DigitalPrice { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Country { get; set; }
        public string PhysicalDescription { get; set; }


        public string AccompanyingMaterials { get; set; }
        public int AccompanyingMaterialsNb { get; set; }
        public int VolumeNb { get; set; }
        public string Abstract { get; set; }
        public string Notes { get; set; }


        public Document()
        { }

        public Document(string pEditor, string pDoi, string pOriginalTitle, string pSubtitle, string pForeword, string pKeywords, string pOriginalLanguage, string pState, Decimal pPrice, DateTime pPublicationDate, string pCountry, string pPhysicalDescription, string pAccompanyingMaterials, int pAccompanyingMaterialsNb, int pVolumeNb, string pAbstract, string pNotes)
        {


            this.Editor = pEditor;
            this.Doi = pDoi;

            this.OriginalTitle = pOriginalTitle;
            this.Subtitle = pSubtitle;
            this.Foreword = pForeword;
            this.Keywords = pKeywords;
            this.OriginalLanguage = pOriginalLanguage;
            this.State = pState;

            this.Price = pPrice;
            this.PublicationDate = pPublicationDate;
            this.Country = pCountry;
            this.PhysicalDescription = pPhysicalDescription;

            this.AccompanyingMaterials = pAccompanyingMaterials;
            this.AccompanyingMaterialsNb = pAccompanyingMaterialsNb;
            this.VolumeNb = pVolumeNb;
            this.Abstract = pAbstract;
            this.Notes = pNotes;
        }
    }
}
