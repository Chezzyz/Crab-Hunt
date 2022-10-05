namespace Characters.Players
{
    public class PlayerData
    {
        public string Name { get; }
        public int SkinVariant { get; }

        public PlayerData(string name, int skin)
        {
            Name = name;
            SkinVariant = skin;
        }
    }
}