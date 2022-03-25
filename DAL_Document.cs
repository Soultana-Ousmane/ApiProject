using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.DAL
{
    public class DAL_Document
    {
        private static bool CheckDocumentUnicity(Document document)
        {
            bool result = false;
            List<Document> documents = getAllDocumentBy("Doi", document.Doi).FindAll(x => x.OriginalTitle == document.OriginalTitle);
            try
            {
                if (document.Id == 0)
                {
                    //Creation
                    if (documents.Count() > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    //Modification
                    foreach (var item in documents)
                    {
                        if (item.Id != document.Id)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch { }
            return result;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        protected static int CountDocumentUnicity(string Doi)
        {
            int NbOccs = 0;
            try
            {
                using (SqlConnection Connection = DBConnection.GetConnection())
                {
                    string StrSQL = "select * from Document where Doi = @Doi";
                    SqlCommand Cmd = new SqlCommand(StrSQL, Connection);
                    Cmd.Parameters.AddWithValue("@Doi", Doi);
                    SqlDataAdapter da = new SqlDataAdapter(Cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    NbOccs = dt.Rows.Count;
                }
            }
            catch { }
            return NbOccs;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        protected static void CreateTable()
        {
            try
            {
                SqlConnection cnn = DBConnection.GetConnection();
                cnn.Open();
                string sql = "If not exists (select * from sysobjects where name = 'Document') CREATE TABLE [dbo].[Document] ([Id] BIGINT IDENTITY (1, 1) NOT NULL,[IdCollection] BIGINT NULL,[Editor] NVARCHAR (50)  NULL,[Doi] NVARCHAR (50)  NULL,[OriginalTitle] NVARCHAR (50)  NULL,[TitlesVariants] NVARCHAR (50)  NULL,[Subtitle] NVARCHAR (50)  NULL,[Foreword]  NVARCHAR (50)  NULL,[Keywords]                NVARCHAR (50)  NULL,[Fichier]                 NVARCHAR (50)  NULL,[FileFormat] NVARCHAR (50)  NULL,[CoverPage] NVARCHAR (50)  NULL,[Url] NVARCHAR (255) NULL,[DocumentType]            NVARCHAR (50)  NULL,[OriginalLanguage]        NVARCHAR (50)  NULL,[LanguagesVariants]       NVARCHAR (100) NULL,[Translator] NVARCHAR (50)  NULL,[AccessType] NVARCHAR (50)  NULL,[State] NVARCHAR (50)  NULL,[Price] DECIMAL (18) NULL,[SellingPrice] DECIMAL (18) NULL,[DigitalPrice] DECIMAL (18)   NULL,[PublicationDate] DATETIME NULL,[Country]  NVARCHAR (50)  NULL,[PhysicalDescription] NVARCHAR (50)  NULL,[AccompanyingMaterials] NVARCHAR (50)  NULL,[AccompanyingMaterialsNb] INT NULL,[VolumeNb]  INT NULL,[Abstract] NVARCHAR (50)  NULL,[Notes] NVARCHAR (50)  NULL,CONSTRAINT [PK_Document_1] PRIMARY KEY CLUSTERED ([Id] ASC),CONSTRAINT [FK_Document_Document] FOREIGN KEY ([IdCollection]) REFERENCES [dbo].[Collection] ([IdCollection]));";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
                cnn.Close();
            }
            catch { }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public static List<Document> getAllDocument()
        {
            List<Document> listDocument = new List<Document>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from Document";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Document d = new Document();
                                d.Id = Int32.Parse(dataReader["Id"].ToString());
                                d.IdCollection = Int32.Parse(dataReader["IdCollection"].ToString());
                                d.Editor = dataReader["Editor"].ToString();
                                d.Doi = dataReader["Doi"].ToString();


                                d.OriginalTitle = dataReader["OriginalTitle"].ToString();
                                d.TitlesVariants = dataReader["TitlesVariants"].ToString();
                                d.Subtitle = dataReader["Subtitle"].ToString();
                                d.Foreword = dataReader["Foreword"].ToString();
                                d.Keywords = dataReader["Keywords"].ToString();
                                d.FileName = dataReader["Fichier"].ToString();
                                d.FileFormat = dataReader["FileFormat"].ToString();
                                d.CoverPage = dataReader["CoverPage"].ToString();
                                d.Url = dataReader["Url"].ToString();
                                d.DocumentType = dataReader["DocumentType"].ToString();
                                d.OriginalLanguage = dataReader["OriginalLanguage"].ToString();
                                d.LanguagesVariants = dataReader["LanguagesVariants"].ToString();
                                d.Translator = dataReader["Translator"].ToString();
                                d.AccessType = dataReader["AccessType"].ToString();
                                d.State = dataReader["State"].ToString();


                                d.Price = Decimal.Parse(dataReader["Price"].ToString());
                                d.SellingPrice = Decimal.Parse(dataReader["SellingPrice"].ToString());
                                d.DigitalPrice = Decimal.Parse(dataReader["DigitalPrice"].ToString());
                                if (dataReader["PublicationDate"].ToString() != "")
                                {
                                    d.PublicationDate = DateTime.Parse(dataReader["PublicationDate"].ToString());
                                }
                                d.Country = dataReader["Country"].ToString();
                                d.PhysicalDescription = dataReader["PhysicalDescription"].ToString();


                                d.AccompanyingMaterials = dataReader["AccompanyingMaterials"].ToString();
                                d.AccompanyingMaterialsNb = Int32.Parse(dataReader["AccompanyingMaterialsNb"].ToString());
                                d.VolumeNb = Int32.Parse(dataReader["VolumeNb"].ToString());
                                d.Abstract = dataReader["Abstract"].ToString();
                                d.Notes = dataReader["Notes"].ToString();


                                listDocument.Add(d);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listDocument;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public static Document getDocumentBy(string Field, string Value)
        {
            Document d = new Document();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from Document where [" + Field + "]=@Field";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                d.Id = Int32.Parse(dataReader["Id"].ToString());
                                d.IdCollection = Int32.Parse(dataReader["IdCollection"].ToString());
                                d.Editor = dataReader["Editor"].ToString();
                                d.Doi = dataReader["Doi"].ToString();


                                d.OriginalTitle = dataReader["OriginalTitle"].ToString();
                                d.TitlesVariants = dataReader["TitlesVariants"].ToString();
                                d.Subtitle = dataReader["Subtitle"].ToString();
                                d.Foreword = dataReader["Foreword"].ToString();
                                d.Keywords = dataReader["Keywords"].ToString();
                                d.FileName = dataReader["Fichier"].ToString();
                                d.FileFormat = dataReader["FileFormat"].ToString();
                                d.CoverPage = dataReader["CoverPage"].ToString();
                                d.Url = dataReader["Url"].ToString();
                                d.DocumentType = dataReader["DocumentType"].ToString();
                                d.OriginalLanguage = dataReader["OriginalLanguage"].ToString();
                                d.LanguagesVariants = dataReader["LanguagesVariants"].ToString();
                                d.Translator = dataReader["Translator"].ToString();
                                d.AccessType = dataReader["AccessType"].ToString();
                                d.State = dataReader["State"].ToString();


                                d.Price = Decimal.Parse(dataReader["Price"].ToString());
                                d.SellingPrice = Decimal.Parse(dataReader["SellingPrice"].ToString());
                                d.DigitalPrice = Decimal.Parse(dataReader["DigitalPrice"].ToString());
                                if (dataReader["PublicationDate"].ToString() != "")
                                {
                                    d.PublicationDate = DateTime.Parse(dataReader["PublicationDate"].ToString());
                                }
                                d.Country = dataReader["Country"].ToString();
                                d.PhysicalDescription = dataReader["PhysicalDescription"].ToString();


                                d.AccompanyingMaterials = dataReader["AccompanyingMaterials"].ToString();
                                d.AccompanyingMaterialsNb = Int32.Parse(dataReader["AccompanyingMaterialsNb"].ToString());
                                d.VolumeNb = Int32.Parse(dataReader["VolumeNb"].ToString());
                                d.Abstract = dataReader["Abstract"].ToString();
                                d.Notes = dataReader["Notes"].ToString();

                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return d;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public static List<Document> getAllDocumentBy(string Field, string Value)
        {
            List<Document> listDocument = new List<Document>();
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string sql = "select * from Document where [" + Field + "]=@Field";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Document d = new Document();
                                d.Id = Int32.Parse(dataReader["Id"].ToString());
                                d.IdCollection = Int32.Parse(dataReader["IdCollection"].ToString());
                                d.Editor = dataReader["Editor"].ToString();
                                d.Doi = dataReader["Doi"].ToString();


                                d.OriginalTitle = dataReader["OriginalTitle"].ToString();
                                d.TitlesVariants = dataReader["TitlesVariants"].ToString();
                                d.Subtitle = dataReader["Subtitle"].ToString();
                                d.Foreword = dataReader["Foreword"].ToString();
                                d.Keywords = dataReader["Keywords"].ToString();
                                d.FileName = dataReader["Fichier"].ToString();
                                d.FileFormat = dataReader["FileFormat"].ToString();
                                d.CoverPage = dataReader["CoverPage"].ToString();
                                d.Url = dataReader["Url"].ToString();
                                d.DocumentType = dataReader["DocumentType"].ToString();
                                d.OriginalLanguage = dataReader["OriginalLanguage"].ToString();
                                d.LanguagesVariants = dataReader["LanguagesVariants"].ToString();
                                d.Translator = dataReader["Translator"].ToString();
                                d.AccessType = dataReader["AccessType"].ToString();
                                d.State = dataReader["State"].ToString();


                                d.Price = Decimal.Parse(dataReader["Price"].ToString());
                                d.SellingPrice = Decimal.Parse(dataReader["SellingPrice"].ToString());
                                d.DigitalPrice = Decimal.Parse(dataReader["DigitalPrice"].ToString());
                                if (dataReader["PublicationDate"].ToString() != "")
                                {
                                    d.PublicationDate = DateTime.Parse(dataReader["PublicationDate"].ToString());
                                }
                                d.Country = dataReader["Country"].ToString();
                                d.PhysicalDescription = dataReader["PhysicalDescription"].ToString();


                                d.AccompanyingMaterials = dataReader["AccompanyingMaterials"].ToString();
                                d.AccompanyingMaterialsNb = Int32.Parse(dataReader["AccompanyingMaterialsNb"].ToString());
                                d.VolumeNb = Int32.Parse(dataReader["VolumeNb"].ToString());
                                d.Abstract = dataReader["Abstract"].ToString();
                                d.Notes = dataReader["Notes"].ToString();

                                listDocument.Add(d);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch { }
            return listDocument;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public static JsonResponse AddDocument(Document document)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur d'ajout";
            try
            {
                CreateTable();
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    if (CheckDocumentUnicity(document) == false)
                    {
                        string sql = "Insert into Document (IdCollection, Editor, Doi, OriginalTitle, TitlesVariants, Subtitle, Foreword, Keywords, Fichier, FileFormat, CoverPage, Url, DocumentType, OriginalLanguage, LanguagesVariants, Translator, AccessType, State, Price, SellingPrice, DigitalPrice, PublicationDate, Country, PhysicalDescription, AccompanyingMaterials, AccompanyingMaterialsNb, VolumeNb, Abstract, Notes)values(@IdCollection, @Editor, @Doi, @OriginalTitle, @TitlesVariants, @Subtitle, @Foreword, @Keywords, @Fichier, @FileFormat, @CoverPage, @Url, @DocumentType, @OriginalLanguage, @LanguagesVariants, @Translator, @AccessType, @State, @Price, @SellingPrice, @DigitalPrice, @PublicationDate, @Country, @PhysicalDescription, @AccompanyingMaterials, @AccompanyingMaterialsNb, @VolumeNb, @Abstract, @Notes)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;
                            if (document.IdCollection == 0)
                                command.Parameters.AddWithValue("@IdCollection", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@IdCollection", document.IdCollection);
                            if (String.IsNullOrEmpty(document.Editor))
                                command.Parameters.AddWithValue("@Editor", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Editor", document.Editor);
                            if (String.IsNullOrEmpty(document.Doi))
                                command.Parameters.AddWithValue("@Doi", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Doi", document.Doi);



                            if (String.IsNullOrEmpty(document.OriginalTitle))
                                command.Parameters.AddWithValue("@OriginalTitle", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@OriginalTitle", document.OriginalTitle);
                            if (String.IsNullOrEmpty(document.TitlesVariants))
                                command.Parameters.AddWithValue("@TitlesVariants", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@TitlesVariants", document.TitlesVariants);
                            if (String.IsNullOrEmpty(document.Subtitle))
                                command.Parameters.AddWithValue("@Subtitle", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Subtitle", document.Subtitle);
                            if (String.IsNullOrEmpty(document.Foreword))
                                command.Parameters.AddWithValue("@Foreword", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Foreword", document.Foreword);
                            if (String.IsNullOrEmpty(document.Keywords))
                                command.Parameters.AddWithValue("@Keywords", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Keywords", document.Keywords);
                            if (String.IsNullOrEmpty(document.FileName))
                                command.Parameters.AddWithValue("@Fichier", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Fichier", document.FileName);
                            if (String.IsNullOrEmpty(document.FileFormat))
                                command.Parameters.AddWithValue("@FileFormat", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@FileFormat", document.FileFormat);
                            if (String.IsNullOrEmpty(document.CoverPage))
                                command.Parameters.AddWithValue("@CoverPage", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@CoverPage", document.CoverPage);
                            if (String.IsNullOrEmpty(document.Url))
                                command.Parameters.AddWithValue("@Url", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Url", document.Url);
                            if (String.IsNullOrEmpty(document.DocumentType))
                                command.Parameters.AddWithValue("@DocumentType", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@DocumentType", document.DocumentType);
                            if (String.IsNullOrEmpty(document.OriginalLanguage))
                                command.Parameters.AddWithValue("@OriginalLanguage", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@OriginalLanguage", document.OriginalLanguage);
                            if (String.IsNullOrEmpty(document.LanguagesVariants))
                                command.Parameters.AddWithValue("@LanguagesVariants", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@LanguagesVariants", document.LanguagesVariants);
                            if (String.IsNullOrEmpty(document.Translator))
                                command.Parameters.AddWithValue("@Translator", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Translator", document.Translator);
                            if (String.IsNullOrEmpty(document.AccessType))
                                command.Parameters.AddWithValue("@AccessType", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@AccessType", document.AccessType);
                            if (String.IsNullOrEmpty(document.State))
                                command.Parameters.AddWithValue("@State", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@State", document.State);



                            if (document.Price == 0)
                                command.Parameters.AddWithValue("@Price", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Price", document.Price);
                            if (document.SellingPrice == 0)
                                command.Parameters.AddWithValue("@SellingPrice", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@SellingPrice", document.SellingPrice);
                            if (document.DigitalPrice == 0)
                                command.Parameters.AddWithValue("@DigitalPrice", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@DigitalPrice", document.DigitalPrice);
                            if (String.IsNullOrEmpty(document.PublicationDate.ToString()))
                                command.Parameters.AddWithValue("@PublicationDate", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@PublicationDate", document.PublicationDate);
                            if (String.IsNullOrEmpty(document.Country))
                                command.Parameters.AddWithValue("@Country", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Country", document.Country);
                            if (String.IsNullOrEmpty(document.PhysicalDescription))
                                command.Parameters.AddWithValue("@PhysicalDescription", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@PhysicalDescription", document.PhysicalDescription);



                            if (String.IsNullOrEmpty(document.AccompanyingMaterials))
                                command.Parameters.AddWithValue("@AccompanyingMaterials", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@AccompanyingMaterials", document.AccompanyingMaterials);
                            if (document.AccompanyingMaterialsNb == 0)
                                command.Parameters.AddWithValue("@AccompanyingMaterialsNb", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@AccompanyingMaterialsNb", document.AccompanyingMaterialsNb);
                            if (document.VolumeNb == 0)
                                command.Parameters.AddWithValue("@VolumeNb", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@VolumeNb", document.VolumeNb);
                            if (String.IsNullOrEmpty(document.Abstract))
                                command.Parameters.AddWithValue("@Abstract", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Abstract", document.Abstract);
                            if (String.IsNullOrEmpty(document.Notes))
                                command.Parameters.AddWithValue("@Notes", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Notes", document.Notes);


                            if (command.ExecuteNonQuery() == 1)
                            {
                                jr.success = true;
                                jr.message = "Ajout ok";
                            }
                        }
                    }
                    else
                    {
                        jr.message = "Document déjà existe !";
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                jr.message = "Erreur : " + e.Message;
            }
            return jr;
        }
        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public static JsonResponse UpdateDocument(Document document)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (CheckDocumentUnicity(document) == false)
                    {
                        connection.Open();
                        string sql = "Update Document set IdCollection=@IdCollection,Editor=@Editor,Doi=@Doi,OriginalTitle=@OriginalTitle,TitlesVariants=@TitlesVariants,Subtitle=@Subtitle,Foreword=@Foreword,Keywords=@Keywords,Fichier=@Fichier,FileFormat=@FileFormat,CoverPage=@CoverPage,Url=@Url,DocumentType=@DocumentType,OriginalLanguage=@OriginalLanguage,LanguagesVariants=@LanguagesVariants,Translator=@Translator,AccessType=@AccessType,State=@State, Price=@Price,SellingPrice=@SellingPrice,DigitalPrice=@DigitalPrice,PublicationDate=@PublicationDate, Country=@Country,PhysicalDescription=@PhysicalDescription, AccompanyingMaterials=@AccompanyingMaterials,AccompanyingMaterialsNb=@AccompanyingMaterialsNb, VolumeNb=@VolumeNb, Abstract=@Abstract, Notes=@Notes   where Id=@Id";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;

                            if (document.IdCollection == 0)
                                command.Parameters.AddWithValue("@IdCollection", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@IdCollection", document.IdCollection);
                            if (String.IsNullOrEmpty(document.Editor))
                                command.Parameters.AddWithValue("@Editor", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Editor", document.Editor);
                            if (String.IsNullOrEmpty(document.Doi))
                                command.Parameters.AddWithValue("@Doi", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Doi", document.Doi);



                            if (String.IsNullOrEmpty(document.OriginalTitle))
                                command.Parameters.AddWithValue("@OriginalTitle", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@OriginalTitle", document.OriginalTitle);
                            if (String.IsNullOrEmpty(document.TitlesVariants))
                                command.Parameters.AddWithValue("@TitlesVariants", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@TitlesVariants", document.TitlesVariants);
                            if (String.IsNullOrEmpty(document.Subtitle))
                                command.Parameters.AddWithValue("@Subtitle", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Subtitle", document.Subtitle);
                            if (String.IsNullOrEmpty(document.Foreword))
                                command.Parameters.AddWithValue("@Foreword", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Foreword", document.Foreword);
                            if (String.IsNullOrEmpty(document.Keywords))
                                command.Parameters.AddWithValue("@Keywords", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Keywords", document.Keywords);
                            if (String.IsNullOrEmpty(document.FileName))
                                command.Parameters.AddWithValue("@Fichier", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Fichier", document.FileName);
                            if (String.IsNullOrEmpty(document.FileFormat))
                                command.Parameters.AddWithValue("@FileFormat", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@FileFormat", document.FileFormat);
                            if (String.IsNullOrEmpty(document.CoverPage))
                                command.Parameters.AddWithValue("@CoverPage", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@CoverPage", document.CoverPage);
                            if (String.IsNullOrEmpty(document.Url))
                                command.Parameters.AddWithValue("@Url", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Url", document.Url);
                            if (String.IsNullOrEmpty(document.DocumentType))
                                command.Parameters.AddWithValue("@DocumentType", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@DocumentType", document.DocumentType);
                            if (String.IsNullOrEmpty(document.OriginalLanguage))
                                command.Parameters.AddWithValue("@OriginalLanguage", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@OriginalLanguage", document.OriginalLanguage);
                            if (String.IsNullOrEmpty(document.LanguagesVariants))
                                command.Parameters.AddWithValue("@LanguagesVariants", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@LanguagesVariants", document.LanguagesVariants);
                            if (String.IsNullOrEmpty(document.Translator))
                                command.Parameters.AddWithValue("@Translator", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Translator", document.Translator);
                            if (String.IsNullOrEmpty(document.AccessType))
                                command.Parameters.AddWithValue("@AccessType", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@AccessType", document.AccessType);
                            if (String.IsNullOrEmpty(document.State))
                                command.Parameters.AddWithValue("@State", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@State", document.State);



                            if (document.Price == 0)
                                command.Parameters.AddWithValue("@Price", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Price", document.Price);
                            if (document.SellingPrice == 0)
                                command.Parameters.AddWithValue("@SellingPrice", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@SellingPrice", document.SellingPrice);
                            if (document.DigitalPrice == 0)
                                command.Parameters.AddWithValue("@DigitalPrice", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@DigitalPrice", document.DigitalPrice);
                            if (String.IsNullOrEmpty(document.PublicationDate.ToString()))
                                command.Parameters.AddWithValue("@PublicationDate", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@PublicationDate", document.PublicationDate);
                            if (String.IsNullOrEmpty(document.Country))
                                command.Parameters.AddWithValue("@Country", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Country", document.Country);
                            if (String.IsNullOrEmpty(document.PhysicalDescription))
                                command.Parameters.AddWithValue("@PhysicalDescription", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@PhysicalDescription", document.PhysicalDescription);



                            if (String.IsNullOrEmpty(document.AccompanyingMaterials))
                                command.Parameters.AddWithValue("@AccompanyingMaterials", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@AccompanyingMaterials", document.AccompanyingMaterials);
                            if (document.AccompanyingMaterialsNb == 0)
                                command.Parameters.AddWithValue("@AccompanyingMaterialsNb", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@AccompanyingMaterialsNb", document.AccompanyingMaterialsNb);
                            if (document.VolumeNb == 0)
                                command.Parameters.AddWithValue("@VolumeNb", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@VolumeNb", document.VolumeNb);
                            if (String.IsNullOrEmpty(document.Abstract))
                                command.Parameters.AddWithValue("@Abstract", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Abstract", document.Abstract);
                            if (String.IsNullOrEmpty(document.Notes))
                                command.Parameters.AddWithValue("@Notes", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@Notes", document.Notes);


                            command.Parameters.AddWithValue("@Id", document.Id);


                            //if (CountDocumentUnicity(document.Doi) > 1)
                            //{
                            //    DeleteDocumentBy("Id", document.Id.ToString());
                            //    AddDocument(ancienDocument);
                            //    jr.message = "Doi déjà existe !";
                            //}
                            //else
                            //{
                            //    jr.success = true;
                            //    jr.message = "Modification effectuée !";
                            //}

                            if (command.ExecuteNonQuery() == 1)
                            {
                                jr.success = true;
                                jr.message = "Modification effectuée !";
                            }
                        }
                        connection.Close();
                    }
                    else
                    {
                        jr.message = "Document déjà existe !";
                    }
                }
            }
            catch (Exception e)
            {
                jr.message = "Erreur : " + e.Message;
            }
            return jr;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public static JsonResponse DeleteDocumentBy(string Field, string Value)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";

            /*
            Document document = getDocumentBy(Field, Value);
            DAL_DocumentFiles.DeleteDocumentFilesBy("IdDocument", document.IdDocument.ToString());
            */

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "delete from Document where [" + Field + "]=@Field";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Field", Value);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            jr.success = true;
                            jr.message = "Suppression effectuée";
                        }
                    }

                    connection.Close();

                }
            }
            catch (Exception e)
            {
                jr.message = "Erreur : " + e.Message;
            }
            return jr;
        }
    }
}
