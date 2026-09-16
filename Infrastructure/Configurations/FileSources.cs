namespace Infrastructure.Configurations;
public class FileSources
{
    public FileSources()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..", "..", ".."));

        HighscoreFileSource = Path.Combine(projectRoot, "Infrastructure", "JsonFiles", "highscore.json");
    }
    public string HighscoreFileSource { get; set; }

}
