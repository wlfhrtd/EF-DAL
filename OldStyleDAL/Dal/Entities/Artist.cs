using System;
using System.Collections.Generic;


namespace Dal.Entities
{
    public partial class Artist
    {
        public Artist()
        {
            ArtistSkills = new HashSet<ArtistSkill>();
        }


        public int ArtistId { get; set; }
        public Guid? OldUserId { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string WebSite { get; set; }
        public byte ProfilePrivacyLevel { get; set; }
        public byte ContactPrivacyLevel { get; set; }
        public long ProfileViews { get; set; }
        public DateTime? ProfileLastViewDate { get; set; }
        public byte? Rating { get; set; }
        public string AvatarUrl { get; set; }
        public string EmailAddress { get; set; }
        public int FileUploadsInBytes { get; set; }
        public int FileUploadQuotaInBytes { get; set; }
        public DateTime LastActivityDate { get; set; }
        public bool? ShowChatStatus { get; set; }
        public bool? AllowChatSounds { get; set; }

        public virtual ICollection<ArtistSkill> ArtistSkills { get; set; }
    }
}
