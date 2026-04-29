using System.ComponentModel.DataAnnotations;

namespace Entities
{
    // User sınıfı, kullanıcı bilgilerini ve yetkilendirme durumunu temsil eder.
    // Her kullanıcı, bir kullanıcı adı, şifre, e-posta, ad, soyad ve admin durumu gibi bilgileri içerir.
    // Bu sınıf, kullanıcıların sisteme giriş yapabilmesi ve yönetimsel işlemleri gerçekleştirebilmesi için kullanılır.
    // Kullanıcılar, admin yetkisine sahip olabilir veya normal kullanıcı olarak tanımlanabilir.
    //Bu sayede sadece yetkili personeller(örneğin Departman Müdürleri) kılavuzları ekleyebilir, düzenleyebilir veya silebilir. Böylece sistemdeki kılavuzların güvenliği sağlanır.
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Kullanıcı Adı"), StringLength(50)]
        public required string UserName { get; set; }
        [Display(Name = "Şifre"), StringLength(100)]
        public required string Password { get; set; }
        [Display(Name = "E-posta"), StringLength(100)]
        public required string Email { get; set; }
        [Display(Name = "Ad"), StringLength(50)]
        public required string FirstName { get; set; }
        [Display(Name = "Soyad"), StringLength(50)]
        public required string LastName { get; set; }
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; } = true;
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
    }
}
