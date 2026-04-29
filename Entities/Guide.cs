using System.ComponentModel.DataAnnotations;

namespace Entities
{
    // Kılavuz sınıfı; makine, ekipmanlar veya Departman Eğitimleri için rehberlik sağlayan belgeleri temsil eder.
    // Her kılavuz, bir departmana ait olabilir ve açıklama, resim ve dosya gibi bilgileri içerebilir.

    // Bu sınıf, kılavuz bilgilerini ve ilişkili departmanı içerir.
    public class Guide
    {
        public int Id{get;set;}


        [Display(Name = "Kılavuz Adı"), StringLength(150)]
        public required string Name{get;set;}


        [Display(Name = "Kılavuz Açıklaması")]
        public string? Description{get;set;}


        [Display(Name = "Kılavuz Resmi")]
        public string? Image{get;set;}


        [Display(Name = "Kılavuz Dosyası")]
        public string? File{get;set;}
        public string? FileType{get;set;
        }


        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? CreatedDate{get;set;}


        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? UpdatedDate{get;set;}


        [Display(Name = "Departman")]
        public int DepartmentId{get;set;}
        public Department? Department{get;set;}
        public ICollection<VideoChapter>? VideoChapters { get; set; }
    }
}
