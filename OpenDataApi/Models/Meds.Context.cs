﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace OpenDataApi.Models
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using System.Data.Entity.Core.Objects;
using System.Linq;


public partial class MedsEntities : DbContext
{
    public MedsEntities()
        : base("name=MedsEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<BNFCode> BNFCodes { get; set; }

    public virtual DbSet<GPPractice> GPPractices { get; set; }

    public virtual DbSet<GPPrescribing> GPPrescribings { get; set; }

    public virtual DbSet<MonthlyPrescriptionTotal> MonthlyPrescriptionTotals { get; set; }

    public virtual DbSet<PracticeTotalsPerMonth> PracticeTotalsPerMonths { get; set; }

    public virtual DbSet<DrugPracticeAndChapterSpend> DrugPracticeAndChapterSpends { get; set; }

    public virtual DbSet<BNFChapterDrugSpending> BNFChapterDrugSpendings { get; set; }

    public virtual DbSet<BNFChapterSpendSummary> BNFChapterSpendSummaries { get; set; }

    public virtual DbSet<ChapterSpending> ChapterSpendings { get; set; }

    public virtual DbSet<RegionPracticeDrugTotalSpend> RegionPracticeDrugTotalSpends { get; set; }

    public virtual DbSet<RegionSurgeryBNFSpendSummary> RegionSurgeryBNFSpendSummaries { get; set; }

    public virtual DbSet<vwPracticePartition> vwPracticePartitions { get; set; }


    public virtual ObjectResult<GetDrugUseBySuperOutputArea_Result> GetDrugUseBySuperOutputArea(string drugName, string year, string month)
    {

        var drugNameParameter = drugName != null ?
            new ObjectParameter("DrugName", drugName) :
            new ObjectParameter("DrugName", typeof(string));


        var yearParameter = year != null ?
            new ObjectParameter("Year", year) :
            new ObjectParameter("Year", typeof(string));


        var monthParameter = month != null ?
            new ObjectParameter("Month", month) :
            new ObjectParameter("Month", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDrugUseBySuperOutputArea_Result>("GetDrugUseBySuperOutputArea", drugNameParameter, yearParameter, monthParameter);
    }


    public virtual ObjectResult<GetDrugUseByBNFChapterAndSectionBySuperOutputArea_Result> GetDrugUseByBNFChapterAndSectionBySuperOutputArea(string bNFChapter, string bNFSection, string year, string month)
    {

        var bNFChapterParameter = bNFChapter != null ?
            new ObjectParameter("BNFChapter", bNFChapter) :
            new ObjectParameter("BNFChapter", typeof(string));


        var bNFSectionParameter = bNFSection != null ?
            new ObjectParameter("BNFSection", bNFSection) :
            new ObjectParameter("BNFSection", typeof(string));


        var yearParameter = year != null ?
            new ObjectParameter("Year", year) :
            new ObjectParameter("Year", typeof(string));


        var monthParameter = month != null ?
            new ObjectParameter("Month", month) :
            new ObjectParameter("Month", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDrugUseByBNFChapterAndSectionBySuperOutputArea_Result>("GetDrugUseByBNFChapterAndSectionBySuperOutputArea", bNFChapterParameter, bNFSectionParameter, yearParameter, monthParameter);
    }


    public virtual ObjectResult<GetDrugUseBySuperOutputAreaPatientRatio_Result> GetDrugUseBySuperOutputAreaPatientRatio(string bNFChapter, string bNFSection, string year, string month)
    {

        var bNFChapterParameter = bNFChapter != null ?
            new ObjectParameter("BNFChapter", bNFChapter) :
            new ObjectParameter("BNFChapter", typeof(string));


        var bNFSectionParameter = bNFSection != null ?
            new ObjectParameter("BNFSection", bNFSection) :
            new ObjectParameter("BNFSection", typeof(string));


        var yearParameter = year != null ?
            new ObjectParameter("Year", year) :
            new ObjectParameter("Year", typeof(string));


        var monthParameter = month != null ?
            new ObjectParameter("Month", month) :
            new ObjectParameter("Month", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDrugUseBySuperOutputAreaPatientRatio_Result>("GetDrugUseBySuperOutputAreaPatientRatio", bNFChapterParameter, bNFSectionParameter, yearParameter, monthParameter);
    }

}

}

