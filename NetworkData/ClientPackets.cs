namespace YgoServer.NetworkData
{
    public enum ClientPackets
    {
        GameList = 0,
        RemoveRoom = 1,
        UpdatePlayers = 2,
        LoginAccepted = 3,
        LoginFailed = 4,
        ServerMessage = 5,
        Banned = 6,
        Kicked = 7,
        RegisterAccept = 8,
        RegisterFailed = 9,
        Pong = 10,
        RoomStart = 11
    }
}
