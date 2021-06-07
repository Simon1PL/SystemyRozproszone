using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordApp2.Models
{
    public class DiscordMesssage
    {
        public string id { get; set; }
        public int type { get; set; }
        public string content { get; set; }
        public string channel_id { get; set; }
        public Author author { get; set; }
        public List<Attachment> attachments { get; set; }
        public List<Embed> embeds { get; set; }
        public List<object> mentions { get; set; }
        public List<string> mention_roles { get; set; }
        public bool pinned { get; set; }
        public bool mention_everyone { get; set; }
        public bool tts { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime? edited_timestamp { get; set; }
        public int flags { get; set; }
        public List<object> components { get; set; }
        public List<Reaction> reactions { get; set; }
    }

    public class Author
    {
        public string id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public string discriminator { get; set; }
        public int public_flags { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Attachment
    {
        public string id { get; set; }
        public string filename { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string proxy_url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string content_type { get; set; }
    }

    public class Video
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string proxy_url { get; set; }
    }

    public class Provider
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Thumbnail
    {
        public string url { get; set; }
        public string proxy_url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Embed
    {
        public string type { get; set; }
        public string url { get; set; }
        public Video video { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int? color { get; set; }
        public Author author { get; set; }
        public Provider provider { get; set; }
        public Thumbnail thumbnail { get; set; }
    }

    public class Emoji
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Reaction
    {
        public Emoji emoji { get; set; }
        public int count { get; set; }
        public bool me { get; set; }
    }
}
