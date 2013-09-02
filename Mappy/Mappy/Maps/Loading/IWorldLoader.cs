namespace Mappy.Maps
{
    internal interface IWorldLoader
    {
        World Load();
        World Load(string path);
    }
}
