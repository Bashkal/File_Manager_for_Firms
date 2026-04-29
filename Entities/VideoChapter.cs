using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class VideoChapter
    {
        public int Id { get; set;}

        public int GuideId { get; set;}

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set;}  
        public string Title { get; set; }

        public Guide Guide { get; set; }
    }

}
