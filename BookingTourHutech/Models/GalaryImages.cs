using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingTourHutech.Models
{
    public class GalaryImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGalaryImage { get; set; }
        // Khai báo khóa ngoại để tạo quan hệ với bảng Gallary
        public int IdGallary { get; set; }
        // Chủ đề liên quan đến Gallary
        [ForeignKey("IdGallary")]
        public Gallary Gallary { get; set; }
        // Các thuộc tính khác của hình ảnh, ví dụ:
        public string ImageName { get; set; }
    }
}
