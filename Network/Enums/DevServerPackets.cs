namespace DevProLauncher.Network.Enums
{
    public enum DevServerPackets
    {
        GameList = 0,
        ChannelList = 1,
        Ping = 3,
        Login = 4,
        Register = 5,
        UserList = 6,
        ChatMessage = 8,
        JoinChannel = 9,
        LeaveChannel = 10,
        AddFriend = 12,
        RemoveFriend = 13,
        RequestDuel = 14,
        AcceptDuel = 15,
        RefuseDuel = 16,
        DevPoints = 17,
        ChatCommand = 18,
        DevPointCommand = 19,
        TournamentList = 20,
        TournamentStart = 21,
        TournamentJoin = 22,
        TournamentLeave = 23,
        TournamentIsReady = 24,
        TournamentCreate = 25,
        Stats = 26,
        TeamStats = 27,
        TeamCommand = 28
    }
}
