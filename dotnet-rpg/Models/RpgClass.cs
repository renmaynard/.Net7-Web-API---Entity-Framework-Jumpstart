using System.Text.Json.Serialization;

namespace Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Ninja = 1,
        Samurai = 2,
        Tanker = 3,
        Knight = 4,
        Healer = 5,
        Thief = 6,
        Mage = 7,
        Summoner = 8,
        Tamer = 9,
        Ranger = 10
    }
}