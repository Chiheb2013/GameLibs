namespace Mappy.Worlds
{
    internal interface IWorldLoader
    {
        World Load();
        World Load(string path);
    }
}
