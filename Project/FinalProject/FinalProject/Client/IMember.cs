using System.Collections.Generic;

namespace FinalProject
{
    public interface IMember
    {
        decimal GetWallet();
        string GetUsername();
        string GetFirstName();
        string GetLastName();
        string GetEmail();
        List<string> GetSubs();
    }
}