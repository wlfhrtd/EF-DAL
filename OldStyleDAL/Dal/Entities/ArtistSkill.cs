using System;


namespace Dal.Entities
{
    public partial class ArtistSkill
    {
        public int ArtistTalentId { get; set; }
        public int ArtistId { get; set; }
        public string TalentName { get; set; }
        public int SkillLevel { get; set; }
        public string Details { get; set; }
        public string Styles { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
