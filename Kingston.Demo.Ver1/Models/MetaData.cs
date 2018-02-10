using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
namespace Kingston.Demo.Ver1.Models
{
    public class MetaData
    {

    }
    public class IssueMetaData
    {
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? Pid { get; set; }

        [Display(Name = "標題 : ")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "分類 : ")]
        [Required(AllowEmptyStrings = false)]
        public string Type { get; set; }

        [Display(Name = "次分類 : ")]
        public string SubType { get; set; }

        [Display(Name = "問題 : ")]
        [Required]
        public string Desc { get; set; }

        [Display(Name = "狀態 : ")]
        public int? Status { get; set; }

        [Display(Name = "標籤 : ")]
        public string Tags { get; set; }
        [Display(Name = "建立者 : ")]
        public string KeyInUser { get; set; }
        [Display(Name = "建立日期 : ")]
        [HiddenInput]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? KeyInDate { get; set; }
    }
    [ModelMetadataType(typeof(IssueMetaData))]
    public partial class Issue
    {
        [NotMapped]
        public string StatusName { get; set; }
    }

    public class AcountMetaData
    {
        public int Id { get; set; }
        [DisplayName("使用者名稱")]
        public string UserName { get; set; }
        [DisplayName("使用者帳號")]
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime? KeyInDate { get; set; }
    }
    [ModelMetadataType(typeof(AcountMetaData))]
    public partial class Account
    {

    }
    public class IssueSubMetaData
    {
        [DisplayName("問題回覆")]
        [Required]
        public string Desc { get; set; }
    }
    [ModelMetadataType(typeof(IssueSubMetaData))]
    public partial class IssueSub
    {

    }

    public class IssueFilesMetaData
    {
        
    }
    [ModelMetadataType(typeof(IssueFilesMetaData))]
    public partial  class IssueFiles
    {
        [NotMapped]
        public string fullPath { get { return "/FileUploads/" + this.IssueId +"/"+ this.FileName; } }
    }
}
