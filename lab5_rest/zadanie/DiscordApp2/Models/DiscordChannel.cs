using System;
using System.Collections.Generic;

public class PermissionOverwrite
{
    public string id { get; set; }
    public string type { get; set; }
    public int allow { get; set; }
    public int deny { get; set; }
    public string allow_new { get; set; }
    public string deny_new { get; set; }
}

public class DiscordChannel
{
    public string id { get; set; }
    public string last_message_id { get; set; }
    public DateTime last_pin_timestamp { get; set; }
    public int type { get; set; }
    public string name { get; set; }
    public int position { get; set; }
    public object parent_id { get; set; }
    public string topic { get; set; }
    public string guild_id { get; set; }
    public List<PermissionOverwrite> permission_overwrites { get; set; }
    public bool nsfw { get; set; }
    public int rate_limit_per_user { get; set; }
}