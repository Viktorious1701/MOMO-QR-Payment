using System.ComponentModel.DataAnnotations;

namespace MOMO_QR_Payment.Models
{
    public class CourseInfoModel
    {
        [Required]
        public string CourseInfo { get; set; }
        //Additionals fields
    }
}
