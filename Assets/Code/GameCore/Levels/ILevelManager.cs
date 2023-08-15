namespace GameCore.Levels
{
    public interface ILevelManager
    {
        LevelBase CurrentLevel { get; }
        public void ServerSpawnLevel();
    }
}