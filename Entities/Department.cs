using System.ComponentModel.DataAnnotations;

namespace Entities
{
    //Departman sınıfı, kılavuzların ait olduğu departmanları temsil eder.
    //Her departman, birden fazla kılavuzu içerebilir.
    
    //Bu sınıf, departman bilgilerini ve ilişkili kılavuzları içerir.
    
    public class Department
    {
        public int Id{get;set;}


        [Display(Name = "Departman Adı"),StringLength(150)]
        public required string Name{get;set;}


        [Display(Name = "Departman Açıklaması")]
        public string? Description{get;set;}


        [Display(Name = "Departman Resmi")]
        public string? Image{get;set;}


        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? CreatedDate{get;set;}

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? UpdatedDate{get;set;}
        public List<Guide>? Guides{get;set;}
    }
}
