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
    using System.Collections.Generic;
    
    public partial class ChapterSpending
    {
        public int Year { get; set; }
        public int MONTH { get; set; }
        public string LCG { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> ItemsPrescribed { get; set; }
        public Nullable<decimal> TotalSpent { get; set; }
    }
}
