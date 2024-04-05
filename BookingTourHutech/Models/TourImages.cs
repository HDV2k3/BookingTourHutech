using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingTourHutech.Models
{
    public class TourImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTourImage { get; set; }
        // Khai báo khóa ngoại để tạo quan hệ với bảng Gallary
        public int TourId { get; set; }
        // Chủ đề liên quan đến Gallary
        [ForeignKey("TourId")]
        public Tour Tour { get; set; }
        // Các thuộc tính khác của hình ảnh, ví dụ:
        public string ImageName { get; set; }
    }
}
