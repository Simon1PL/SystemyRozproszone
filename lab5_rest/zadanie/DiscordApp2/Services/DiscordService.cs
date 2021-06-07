using DiscordApp2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DiscordApp2.Services
{
    public interface IDiscordService {
        public Task<List<DiscordChannel>> GetAllChannels();
        public Task<dynamic> GetStats(ChannelModel model);
    }
    public class DiscordService: IDiscordService
    {
        private readonly IHttpClientFactory _clientFactory;
        public DiscordService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<List<DiscordChannel>> GetAllChannels()
        {
                HttpClient client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", "ODQ2MTUyMDYxMzQzMzY3MTg5.YKrWhA.VbB3xRtsTRzSn_UAv5UzU-AEJEY");
                string response = await client.GetStringAsync("https://discord.com/api/guilds/279940808763899904/channels");
                List<DiscordChannel> channels = ((JArray)JsonConvert.DeserializeObject(response)).ToObject<List<DiscordChannel>>();
                List<string> channelsNames = channels.Select(ch => ch.name).ToList();
                return channels;
        }

        public async Task<dynamic> GetStats(ChannelModel model)
        {
            StatsModel stats = new StatsModel();
            stats.ChannelInfo = model;
            if (model.Word == null)
            {
                return stats;
            }
            HttpClient client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", "ODQ2MTUyMDYxMzQzMzY3MTg5.YKrWhA.VbB3xRtsTRzSn_UAv5UzU-AEJEY");
            StatsModel result = null;
            string beforeParam = "";
            if (model.HistoryDeep.Equals("MORE!"))
            {
                model.HistoryDeep = "49000";
            }
            for (int i = 0; i < int.Parse(model.HistoryDeep)/100; i++)
            {
                if (i%5 == 0)
                {
                    System.Threading.Thread.Sleep(5000);
                }
                Task<StatsModel> task = client.GetStringAsync($"https://discord.com/api/channels/{model.Channel}/messages?limit=100" + beforeParam).ContinueWith(res => {
                    List<DiscordMesssage> messages = ((JArray)JsonConvert.DeserializeObject(res.Result)).ToObject<List<DiscordMesssage>>();
                    beforeParam = messages.Any() ? "&before=" + messages.Last().id : "";
                    return CalculateStats(messages, model.Word);
                });
                if (result != null)
                {
                    stats.ResultsAmount += result.ResultsAmount;
                    foreach (UserResult user in result.UserResults)
                    {   
                        stats.AddUserResult(user.User, user.Amount);
                    }
                }
                result = await task;
                if (string.IsNullOrEmpty(beforeParam))
                {
                    break;
                }
            }
            stats.ResultsAmount += result.ResultsAmount;
            foreach (UserResult user in result.UserResults)
            {
                stats.AddUserResult(user.User, user.Amount);
            }
            return stats;
        }

        private StatsModel CalculateStats(List<DiscordMesssage> messages, string word)
        {
            StatsModel statsModel = new StatsModel();
            foreach (DiscordMesssage message in messages)
            {
                if(message.content.ToLower().Contains(word.ToLower()))
                {
                    statsModel.ResultsAmount++;
                    statsModel.AddUserResult(message.author);
                }
            }
            return statsModel;
        }
    }
}
