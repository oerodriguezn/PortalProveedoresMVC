﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortalProveedoresMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PortalProveedoresEntities : DbContext
    {
        public PortalProveedoresEntities()
            : base("name=PortalProveedoresEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BioOrdenesCompra> BioOrdenesCompra { get; set; }
        public virtual DbSet<BioOrdenesCompraDetalle> BioOrdenesCompraDetalle { get; set; }
        public virtual DbSet<BioProveedores> BioProveedores { get; set; }
        public virtual DbSet<BioRetencionFacturas> BioRetencionFacturas { get; set; }
    
        public virtual ObjectResult<BioOrdenesCompraDetalle> BioObtenerDetalleODC(string documento, string rIF)
        {
            var documentoParameter = documento != null ?
                new ObjectParameter("Documento", documento) :
                new ObjectParameter("Documento", typeof(string));
    
            var rIFParameter = rIF != null ?
                new ObjectParameter("RIF", rIF) :
                new ObjectParameter("RIF", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BioOrdenesCompraDetalle>("BioObtenerDetalleODC", documentoParameter, rIFParameter);
        }
    
        public virtual ObjectResult<BioOrdenesCompraDetalle> BioObtenerDetalleODC(string documento, string rIF, MergeOption mergeOption)
        {
            var documentoParameter = documento != null ?
                new ObjectParameter("Documento", documento) :
                new ObjectParameter("Documento", typeof(string));
    
            var rIFParameter = rIF != null ?
                new ObjectParameter("RIF", rIF) :
                new ObjectParameter("RIF", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BioOrdenesCompraDetalle>("BioObtenerDetalleODC", mergeOption, documentoParameter, rIFParameter);
        }
    
        public virtual ObjectResult<BioObtenerOrdenesCompra_Result> BioObtenerOrdenesCompra(string rIF, Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta)
        {
            var rIFParameter = rIF != null ?
                new ObjectParameter("RIF", rIF) :
                new ObjectParameter("RIF", typeof(string));
    
            var fechaDesdeParameter = fechaDesde.HasValue ?
                new ObjectParameter("FechaDesde", fechaDesde) :
                new ObjectParameter("FechaDesde", typeof(System.DateTime));
    
            var fechaHastaParameter = fechaHasta.HasValue ?
                new ObjectParameter("FechaHasta", fechaHasta) :
                new ObjectParameter("FechaHasta", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BioObtenerOrdenesCompra_Result>("BioObtenerOrdenesCompra", rIFParameter, fechaDesdeParameter, fechaHastaParameter);
        }
    }
}