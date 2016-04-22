namespace FinalProject
{
    public class Member : AbstractMember
    {
        public Member(string firstName, string lastName, string email, string userName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = userName;
        }
    }
}