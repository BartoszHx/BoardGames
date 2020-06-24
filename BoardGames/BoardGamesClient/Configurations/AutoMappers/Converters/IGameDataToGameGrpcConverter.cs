using AutoMapper;
using BoardGamesShared.Interfaces;
using GameOnlineGrpc = BoardGamesGrpc.GameOnlines;


namespace BoardGamesClient.Configurations.AutoMappers.Converters
{
    internal class IGameDataToGameGrpcConverter : ITypeConverter<IGameData, GameOnlineGrpc.GameData>
    {
        public GameOnlineGrpc.GameData Convert(IGameData source, GameOnlineGrpc.GameData destination, ResolutionContext context)
        {
            destination = destination ?? new GameOnlineGrpc.GameData();
            destination.Board = context.Mapper.Map<GameOnlineGrpc.Board>(source.Board);
            destination.PlayerTurn = context.Mapper.Map<GameOnlineGrpc.Player>(source.PlayerTurn);

            foreach (var player in source.PlayerList)
            {
                destination.PlayerList.Add(context.Mapper.Map<GameOnlineGrpc.Player>(player));
            }

            foreach(var history in source.PawnHistoriesList)
            {
                destination.PawnHistoriesList.Add(context.Mapper.Map<GameOnlineGrpc.PawnHistory>(history));
            }

            return destination;
        }
    }

}
