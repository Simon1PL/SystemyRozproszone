using DiscordApp2.Models;
using System.Collections.Generic;

public class StatsModel
{
    public ChannelModel ChannelInfo { get; set; }
    public int ResultsAmount { get; set; }
    public List<UserResult> UserResults { get; set; } = new List<UserResult>();

    public void AddUserResult(Author user, int amount = 1)
    {
        UserResult userResult = UserResults.Find(r => r.User.id == user.id);
        if (userResult != null)
        {
            userResult.Amount += amount;
        }
        else
        {
            userResult = new UserResult
            {
                User = user,
                Amount = amount
            };
            UserResults.Add(userResult);
        }
    }
}

public class UserResult
{
    public Author User { get; set; }
    public int Amount { get; set; }
}


