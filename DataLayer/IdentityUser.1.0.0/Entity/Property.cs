namespace IdentityUser100.Entity 
{ 
    public class Property
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public ICollection<UserProperty> UserProperties{ get; set; }
    }
}
