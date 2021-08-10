using System.ComponentModel.DataAnnotations;

namespace YourTaxi.Models
{
    public class Cars
    {
        public int Id { get; set; }

        [Display(Name = "Car Name")]
        public string CarName { get; set; }

        [Display(Name = "CarModel")]
        public string CarModel { get; set; }
        
        [Display(Name = "From")]
        public string From { get; set; }
        
        [Display(Name = "To")]
        public string To { get; set; }
        
        [Display(Name = "Price")]
        public string Price { get; set; }
    }
}
