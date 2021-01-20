using System.ComponentModel.DataAnnotations;

namespace ECommerce.Project.Helper
{
    public class Enums
    {
        public enum SettingTypeData
        {
            [Display(Name = "HTML")] HTML = 1,
            [Display(Name = "FILE")] FILE = 2,
            [Display(Name = "TEXT")] TEXT = 3,
        }

        public enum BannerType
        {
            [Display(Name = "Slider")] Slider = 1,
            [Display(Name = "FILE")] Banner = 2
        }
    }
}