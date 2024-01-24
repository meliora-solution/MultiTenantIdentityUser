namespace IdentityUser100.Entity
{
    public class UserProperty
    {
        public string UserId { get; set; }
        public string PropertyId { get; set; }
        public string PropertyValue { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Property property { get; set; }

    }
}
