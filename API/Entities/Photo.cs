using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        //  Photo storage solution will use the PublicId. Not necessary now.
        public string PublicId { get; set; }
        //  Fully defines the relationship between Photo.cs and AppUser, can be seen via Photo Table migration added in past and then removed.
        //  Can add back once this is added.
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}