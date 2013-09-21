namespace DevProLauncher.Network.Data
{
    public class ChannelUsers
    {
        public string Name { get; set; }
        public UserData[] Users { get; set; }
        public ChannelUsers()
        {
            
        }
        public ChannelUsers(string name, UserData[] users)
        {
            Name = name;
            Users = users;
        }
        public ChannelUsers(string name, UserData user)
        {
            Name = name;
            Users = new UserData[] { user };
        }
    }
}
