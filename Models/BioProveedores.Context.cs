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
    
    public partial class BioMercadosEntities : DbContext
    {
        public BioMercadosEntities()
            : base("name=BioMercadosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MA_PRODUCTOS> MA_PRODUCTOS { get; set; }
        public virtual DbSet<MA_PROVEEDORES> MA_PROVEEDORES { get; set; }
        public virtual DbSet<TR_INVENTARIO> TR_INVENTARIO { get; set; }
        public virtual DbSet<MA_INVENTARIO> MA_INVENTARIO { get; set; }
    }
}
