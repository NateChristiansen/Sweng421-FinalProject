namespace FinalProject
{
    public class Member : AbstractMember
    {
        public Member(string firstName, string lastName, string email, string userName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Username = userName;
        }
    }
}