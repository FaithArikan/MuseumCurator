namespace ArcadeIdle.SaveSystem
{
    public interface IGameSave
    {
        void Save(string filename, object data);
        T Load<T>(string filename) where T : new();
        void Delete(string filename);
        bool FileExists(string filename);
        string GetFullPath(string filename);
        public void WriteToDisk();
    }
}