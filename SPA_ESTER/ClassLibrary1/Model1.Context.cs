﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Spa_EsterEntities : DbContext
    {
        public Spa_EsterEntities()
            : base("name=Spa_EsterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Facturas> Facturas { get; set; }
        public virtual DbSet<Metodos_Pago> Metodos_Pago { get; set; }
        public virtual DbSet<Reservas> Reservas { get; set; }
        public virtual DbSet<Servicios> Servicios { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
