namespace TheGuild.Saving
{
    public interface ISaveable
    {
        void Save(string id);
        void Load(string id);
    }
}