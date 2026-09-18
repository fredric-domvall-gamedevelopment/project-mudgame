namespace Infrastructure.Configurations;

public class FileSources
{
    public FileSources()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..", "..", "..", ".."));

        HighscoreFileSource = Path.Combine(projectRoot, "Infrastructure", "JsonFiles", "highscore.json");
        PlayersFileSource = Path.Combine(projectRoot, "Infrastructure", "JsonFiles", "players.json");
        MountainEnemiesFileSource = Path.Combine(projectRoot, "Infrastructure", "JsonFiles\\Enemies", "mountain-enemies.json");
        SeaEnemiesFileSource = Path.Combine(projectRoot, "Infrastructure", "JsonFiles\\Enemies", "sea-enemies.json");
    }
    public string HighscoreFileSource { get; set; }
    public string PlayersFileSource { get; set; }
    public string MountainEnemiesFileSource { get; set; }
    public string SeaEnemiesFileSource { get; set; }

}
