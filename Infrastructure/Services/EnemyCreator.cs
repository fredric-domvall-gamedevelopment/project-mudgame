using Infrastructure.Configurations;
using Infrastructure.Enums;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class EnemyCreator(JsonFileRepository<Enemy> jsonFileRepository, FileSources fileSources)
{
    private readonly List<Enemy> _enemies = new List<Enemy>();
    private readonly JsonFileRepository<Enemy> _JsonFileRepository = jsonFileRepository;
    private readonly FileSources _filesources = fileSources;

    public Enemy CreateEnemy(Enemy enemyTemplate, EnemyRank enemyRank, Player player)
    {
        Enemy enemy = new Enemy();
        enemy.Name = enemyTemplate.Name;
        enemy.Type = enemyTemplate.Type;
        enemy.Rank = enemyRank;
        enemy.SpawnArea = enemyTemplate.SpawnArea;

        CharacterStatsCalculator characterStatsCalculator = new CharacterStatsCalculator();
        

        switch (enemyRank)
        {
            case EnemyRank.Normal:

                break;

            case EnemyRank.Elite:

                break;

            case EnemyRank.Boss:

                break;

            default:
                throw new ArgumentException("Invalid enemy type");
        }

        return enemy;
    }

    public async Task<Enemy> CreateRandomEnemy(Player player, EnemySpawnArea spawnArea)
    {
        _enemies.Clear();

        await ReadEnemyFromFile(spawnArea);
        
        List<Enemy> enemyOptions = _enemies.Where(e => e.SpawnArea == spawnArea).ToList();


        int randomIndex = new Random().Next(enemyOptions.Count);

        Enemy enemyTemplate = enemyOptions[randomIndex];
        EnemyRank enemyRank = (EnemyRank)Random.Shared.Next(0,3);

        return CreateEnemy(enemyTemplate, enemyRank, player);
    }

    public async Task ReadEnemyFromFile(EnemySpawnArea spawnArea)
    {
        switch (spawnArea)
        {
            case EnemySpawnArea.Mountain:
                var mountainEnemies = await _JsonFileRepository.ReadFromJsonAsync(_filesources.MountainEnemiesFileSource);
                if(mountainEnemies.Data is not null)
                    _enemies.AddRange(mountainEnemies.Data);

                break;
            case EnemySpawnArea.Sea:
                var seaEnemies = await _JsonFileRepository.ReadFromJsonAsync(_filesources.SeaEnemiesFileSource);
                if (seaEnemies.Data is not null)
                    _enemies.AddRange(seaEnemies.Data);

                break;
            default:
                throw new ArgumentException("Invalid enemy spawn area");
        }
    }
}
