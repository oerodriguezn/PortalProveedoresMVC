using Pechkin;
using PortalProveedoresMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pechkin.Util;
using System.Drawing.Printing;
using Pechkin.Synchronized;
using System.Text;
using System.IO;
using RazorPDF;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Xml;
using iTextSharp.text.xml;

namespace PortalProveedoresMVC.Controllers
{
    public class ProveedorController : Controller
    {
        BioMercadosEntities context = new BioMercadosEntities();
        //
        // GET: /Proveedor/
        public ActionResult Index()
        {
            return View();
        }

        public string ValidaRIF(string RIF)
        {
            RIF = RIF.Replace("-", "").ToUpper();
            string Mensaje ="";
            var proveedor = context.MA_PROVEEDORES.Where(p => p.c_rif == RIF);
            if (proveedor.Count() == 0)
                Mensaje = "-1|RIF no aparece como proveedor de BioMercados, por favor revise o contactenos.";
            else
                Mensaje = "0|" + proveedor.First().c_razon;
            return    Mensaje;
        }

        public ActionResult Facturas()
        {

            return View("_FacturasProveedor");
        }

        public ActionResult DetalleFacturas()
        {

            return View("_DetalleFacturas");
        }
        public ActionResult Devoluciones()
        {

            return View("_DevolucionesProveedor");
        }
        public ActionResult DatosProveedor()
        {
            return View("_DatosProveedor");
        }

        public ActionResult Consolidado()
        {
            return View("_BioConsolidadoProveedor");
        }

        public ActionResult OrdenCompra(string Documento, string RIF, string PDF)
        {
            if (PDF != null && PDF.ToLower() == "true")
            {
                PortalProveedoresEntities contexto = new PortalProveedoresEntities();
                var proveedor = contexto.BioProveedores.Where(p => p.RIF == RIF).FirstOrDefault();
                var ODC = contexto.BioOrdenesCompra.Where(p => p.Documento == Documento && p.RIF == RIF).FirstOrDefault();
                var DetalleODC = contexto.BioObtenerDetalleODC(Documento, RIF).ToList();

                ViewBag.DetalleODC = DetalleODC;
                ViewBag.Proveedor = proveedor;
                ViewBag.ODC = ODC;
                return View();
            }
            else
            {
             
                return GeneraPDF(Documento, RIF, "Orden de compra", Request.Url.ToString()+"&PDF=true");
            }
        }

         public FileStreamResult GeneraPDF(string Documento, string RIF, string Titulo,string URL)
        {
            // create global configuration object
            GlobalConfig gc = new GlobalConfig();

            // set it up using fluent notation
            gc.SetMargins(new Margins(50,50, 0, 0))
              .SetDocumentTitle(Titulo)
              .SetPaperSize(PaperKind.Letter);
            //... etc

            // create converter
            IPechkin pechkin = new SynchronizedPechkin(gc);

 
            // create document configuration object
            ObjectConfig oc = new ObjectConfig();

            // and set it up using fluent notation too
            oc.SetCreateExternalLinks(false)
              .SetFallbackEncoding(Encoding.ASCII)
              .SetLoadImages(true)
              .SetPageUri(URL.Replace("GeneraPDF", ""));
            //... etc

            // convert document
            byte[] pdfBuf = pechkin.Convert(oc);

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + Titulo + "  " + Documento + ".pdf");
            MemoryStream ms = new MemoryStream(pdfBuf);
            return new FileStreamResult(ms, "application/pdf");
        }

   
        public ActionResult Retencion(string Documento, string RIF, string PDF,int TipoRetencion)
        {
            if (PDF != null && PDF.ToLower() == "true")
            {
                PortalProveedoresEntities contexto = new PortalProveedoresEntities();
                var proveedor = contexto.BioProveedores.Where(p => p.RIF == RIF).FirstOrDefault();
                var Retencion = contexto.BioRetencionFacturas.Where(p => p.Factura == Documento && p.RIF == RIF && p.CodRetencion == TipoRetencion).FirstOrDefault();
                
                ViewBag.MontoLetras = Numalet.ToCardinal(Retencion.Retencion);
                ViewBag.Proveedor = proveedor;
                ViewBag.Retencion = Retencion;
                return View("Retencion");
            }
            else
            {

                return GeneraPDF(Documento, RIF, "Comprobante Retención ", Request.Url.ToString() + "&PDF=true");
            }
        }

        public string LinksRetencion(string Documento, string RIF)
        {
            StringBuilder sb = new StringBuilder();
            PortalProveedoresEntities contexto = new PortalProveedoresEntities();
            var Retencion = contexto.BioRetencionFacturas.Where(p => p.Factura == Documento && p.RIF == RIF);
            string URL = Request.Url.ToString().Replace("LinksRetencion", "Retencion").Replace(Request.Url.Query,"");
            if (Retencion.Count() == 0)
                sb.Append("No Existen Retenciones");
            foreach (var item in Retencion)
            {
                sb.AppendFormat("<a href=\"{0}?Documento={1}&RIF={2}&TipoRetencion={3}\">{4}</a><br/>", URL,Documento,RIF,item.CodRetencion, item.ConceptoRetencion.Replace("RETENCION","").Trim());
            }
            return sb.ToString();
        }
        
        
	}
}