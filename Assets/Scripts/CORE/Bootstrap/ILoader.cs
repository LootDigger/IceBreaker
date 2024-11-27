using Cysharp.Threading.Tasks;

public interface ILoader
{
    UniTask Init();
    bool IsInitialized { get; set; }
}
