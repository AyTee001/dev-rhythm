namespace DevRhythm.Web.Options
{
    public class DbConnectionOptions
    {
        public const string Connections = "ConnectionStrings";
        public string DevRhythmDbConnection { get; set; } = string.Empty;
        public string HangfireDbConnection { get; set; } = string.Empty;
    }
}
