﻿using Newtonsoft.Json;
using System;

namespace Discord.API
{
    public class Message
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [JsonProperty("author")]
        public Optional<User> Author { get; set; }
        [JsonProperty("content")]
        public Optional<string> Content { get; set; }
        [JsonProperty("timestamp")]
        public Optional<DateTime> Timestamp { get; set; }
        [JsonProperty("edited_timestamp")]
        public Optional<DateTime?> EditedTimestamp { get; set; }
        [JsonProperty("tts")]
        public Optional<bool> IsTextToSpeech { get; set; }
        [JsonProperty("mention_everyone")]
        public Optional<bool> IsMentioningEveryone { get; set; }
        [JsonProperty("mentions")]
        public Optional<User[]> Mentions { get; set; }
        [JsonProperty("attachments")]
        public Optional<Attachment[]> Attachments { get; set; }
        [JsonProperty("embeds")]
        public Optional<Embed[]> Embeds { get; set; }
    }
}
