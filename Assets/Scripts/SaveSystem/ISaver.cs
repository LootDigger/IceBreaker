public interface ISaver
{
    void SaveData<T>(T obj,string path) where T : class;
    T LoadData<T>(string path) where T : class;
}
