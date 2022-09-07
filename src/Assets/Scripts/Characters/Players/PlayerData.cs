namespace Characters.Players
{
    public struct PlayerData
    {
        public string Name { get; set; }
        public int SkinVariant { get; set; }

        public PlayerData(string name, int skin)
        {
            Name = name;
            SkinVariant = skin;
        }
    }
}