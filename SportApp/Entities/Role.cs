namespace SportApp.Entities
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
    }
}
