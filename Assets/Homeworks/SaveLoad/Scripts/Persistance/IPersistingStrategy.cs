namespace Homeworks.SaveLoad.Scripts.Persistance
{
    public interface IPersistingStrategy
    {
        public void Save(string valueToSave);
        public bool TryLoad(out string value);
    }
}