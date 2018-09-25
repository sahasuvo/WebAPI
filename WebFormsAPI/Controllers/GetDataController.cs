using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebFormsAPI.Models;
using System.Web.Script.Serialization;
using System.Data;
using System.Web;
using System.Web.Http.Cors;
using WebFormsAPI.Class;

namespace WebFormsAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetDataController : ApiController
    {

        //Products[] products = new Products[] 
        //{ 
        //    new Products { ID = "1", Name = "Tomato Soup", Category = "Groceries", Price = 1 }, 
        //    new Products { ID = "2", Name = "Yo-yo", Category = "Toys", Price = 3.75M }, 
        //    new Products { ID = "3", Name = "Hammer", Category = "Hardware", Price = 16.99M } 
        //};

        //DataTransfer objdt = new DataTransfer();

        //public DataTransfer GetAllProducts()
        //{
        //    return objdt.GetData();
        //}

        //public IEnumerable<Products> GetAllProducts()
        //{
        //    return products;
        //}

        //public IHttpActionResult GetProduct(String id)
        //{
        //    var product = products.FirstOrDefault((p) => p.ID == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}

        /*
        [HttpGet]
        public IEnumerable<Products> HelloData()
        {
            //DataTransfer objNewData = new DataTransfer();
            //String strData = objNewData.GetData();
            //if(strData.Length > 0)
            //{
            //    return Ok(strData);
            //}

            //return NotFound(strData);
            //return new JavaScriptSerializer().Deserialize<String>(objNewData.GetData());
            //return 

            System.Data.DataSet dtd = new System.Data.DataSet();
            dtd.ReadXml(HttpContext.Current.Server.MapPath("~/App_Data/DropDownMsgList.xml"));
            JavaScriptSerializer jsontest = new JavaScriptSerializer();

            List<Products> objNewData = new List<Products>();
            if (dtd != null)
            {
                objNewData = (from p in dtd.Tables[0].AsEnumerable()
                              select new Products()
                              {
                                  ID = p.Field<String>("SelTabID"),
                                  Name = p.Field<String>("DDLMsg")
                              }).ToList();
            }

            return objNewData;
        }
        */



        //public HttpResponseMessage GetXmlData()
        //{
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    response.StatusCode = HttpStatusCode.OK;
        //    var formatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
        //    #region old
        //    //if (SessionPrincipal.UserId != null)
        //    //{
        //    //    UserId = SessionPrincipal.UserId.ToString();
        //    //}

        //    /*
        //    CompanyService oCompanyService = new CompanyService();

        //    Guid objguid = new Guid(UserId);
        //    List<ParentCompany> comp = oCompanyService.GetAllTypeProcessCompanyRecord(objguid);
        //    comp.Sort((x, y) => string.Compare(x.CompanyName, y.CompanyName));

        //    if (comp != null)
        //    {
        //        response.StatusCode = HttpStatusCode.OK;
        //        response.Content = new ObjectContent<List<ParentCompany>>(comp, formatter);
        //    }
        //    else
        //    {
        //        response.StatusCode = HttpStatusCode.NotFound;
        //    }
        //    */

        //    #endregion old
        //    System.Data.DataSet dtd = new System.Data.DataSet();
        //    dtd.ReadXml(HttpContext.Current.Server.MapPath("~/App_Data/DropDownMsgList.xml"));
        //    JavaScriptSerializer jsontest = new JavaScriptSerializer();

        //    List<Products> objNewData = new List<Products>();
        //    if (dtd != null)
        //    {
        //        objNewData = (from p in dtd.Tables[0].AsEnumerable()
        //                      select new Products()
        //                      {
        //                          ID = p.Field<String>("SelTabID"),
        //                          Name = p.Field<String>("DDLMsg")
        //                      }).ToList();
        //    }


        //    if(objNewData!=null)
        //    {
        //        response.StatusCode = HttpStatusCode.OK;
        //        response.Content = new ObjectContent<List<Products>>(objNewData, formatter);
        //    }
        //    else
        //    {
        //        response.StatusCode = HttpStatusCode.NotFound;
        //    }

        //    response.Headers.Add("Access-Control-Allow-Origin", "*");
           
        //    return response;

        //}
        //[HttpGet]
        //public HttpResponseMessage GetProduct(String i)
        //{
        //    var jsonfrmt = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
        //    HttpResponseMessage resp = new HttpResponseMessage();
        //    resp.Content = new ObjectContent(products.GetType(), products, jsonfrmt);
        //    resp.Headers.Add("Access-Control-Allow-Origin", "");
            
        //    return resp;
        //}

        [HttpGet]
        [ActionName("GetProducts")]
        
        public HttpResponseMessage GetProducts()
        {
            HttpRequestMessage req = new HttpRequestMessage();
            //string str = req.RequestUri.AbsoluteUri;
            var jsonformat = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new ObjectContent<List<Products>>(ProductOps.GetAllProducts(), jsonformat);
            return response;
        }
        [HttpGet]
        [ActionName("GetProductID")]
        public HttpResponseMessage GetProduct(String ID)
        {
            //List<Products> objProd = ProductOps.GetAllProducts().FirstOrDefault(p => p.ID.Equals(ID));
            var prod = ProductOps.GetAllProducts().FirstOrDefault(p => p.ID.Equals(ID));
            var jsonformat = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
            HttpResponseMessage response = new HttpResponseMessage();
            //response.Content = new ObjectContent<List<Products>>(ProductOps.GetProduct(ID), jsonformat);
            response.Content = new ObjectContent(prod.GetType(), prod, jsonformat);
            return response;
        }

    }

    
}
namespace DataTrans
{
    public class DataTransfer
    {
        public string GetData()
        {
            System.Data.DataSet dtd = new System.Data.DataSet();
            dtd.ReadXml(HttpContext.Current.Server.MapPath("~/App_Data/DropDownMsgList.xml"));
            JavaScriptSerializer jsontest = new JavaScriptSerializer();

            List<Products> objNewData = new List<Products>();
            if (dtd != null)
            {
                objNewData = (from p in dtd.Tables[0].AsEnumerable()
                              select new Products()
                              {
                                  ID = p.Field<String>("SelTabID"),
                                  Name = p.Field<String>("DDLMsg")
                              }).ToList();
            }

            return jsontest.Serialize(objNewData);
        }

    }
}
