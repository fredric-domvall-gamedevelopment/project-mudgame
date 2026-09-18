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

        AdjustEnemyStats(enemy, enemyRank, player);

        CharacterStatsCalculator characterStatsCalculator = new CharacterStatsCalculator();
        
        enemy.Attack = characterStatsCalculator.CalculateCharacterAttack(enemy);
        enemy.Defense = characterStatsCalculator.ClalculateCharacterDefense(enemy);
        enemy.MaxHealth = characterStatsCalculator.CalculateCharacterHealth(enemy);
        enemy.Health = enemy.MaxHealth;

        return enemy;
    }

    public async Task<Enemy> CreateRandomEnemy(Player player, EnemySpawnArea spawnArea)
    {
        _enemies.Clear();

        await ReadEnemyFromFile(spawnArea);
        
        List<Enemy> enemyOptions = _enemies.Where(e => e.SpawnArea == spawnArea).ToList();


        int randomIndex = new Random().Next(enemyOptions.Count);

        Enemy enemyTemplate = enemyOptions[randomIndex];
        EnemyRank enemyRank = (EnemyRank)Random.Shared.Next(0, 3);


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

    private void AdjustEnemyStats(Enemy enemy, EnemyRank enemyRank, Player player)
    {
        switch (enemyRank)
        {
            case EnemyRank.Normal:
                enemy.Stats.Strength = (int)Random.Shared.Next((int)Math.Max(1, player.Stats.Strength - 3), (int)player.Stats.Strength + 3);
                enemy.Stats.Dexterity = (int)Random.Shared.Next((int)Math.Max(1, player.Stats.Dexterity - 3), (int)player.Stats.Dexterity + 3);
                enemy.Stats.Endurance = (int)Random.Shared.Next((int)Math.Max(1, player.Stats.Endurance - 3), (int)player.Stats.Endurance + 3);
                break;
            case EnemyRank.Elite:
                enemy.Stats.Strength = (int)Random.Shared.Next((int)Math.Max(1, player.Stats.Strength - 2), (int)player.Stats.Strength + 5);
                enemy.Stats.Dexterity = (int)Random.Shared.Next((int)Math.Max(1, player.Stats.Dexterity - 2), (int)player.Stats.Dexterity + 5);
                enemy.Stats.Endurance = (int)Random.Shared.Next((int)Math.Max(1, player.Stats.Endurance - 2), (int)player.Stats.Endurance + 5);
                break;
            case EnemyRank.Boss:
                enemy.Stats.Strength = (int)Random.Shared.Next((int)Math.Max(1, player.Stats.Strength - 1), (int)player.Stats.Strength + 7);
                enemy.Stats.Dexterity = (int)Random.Shared.Next((int)Math.Max(1, player.Stats.Dexterity - 1), (int)player.Stats.Dexterity + 7);
                enemy.Stats.Endurance = (int)Random.Shared.Next((int)Math.Max(1, player.Stats.Endurance - 1), (int)player.Stats.Endurance + 7); 
                break;
            default:
                throw new ArgumentException("Invalid enemy rank");
        }
    }
}
