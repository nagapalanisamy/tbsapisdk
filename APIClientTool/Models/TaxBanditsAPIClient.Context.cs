﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIClientTool.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class TaxBanditsAPIClientEntities : DbContext
    {
        public TaxBanditsAPIClientEntities()
            : base("name=TaxBanditsAPIClientEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<APIResponse> APIResponses { get; set; }
        public virtual DbSet<ErrorStatu> ErrorStatus { get; set; }
        public virtual DbSet<RecordError> RecordErrors { get; set; }
        public virtual DbSet<SuccessStatu> SuccessStatus { get; set; }
    }
}
