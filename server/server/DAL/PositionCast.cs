namespace server.DAL
{
    partial class position
    {
        public static explicit operator Common.Model.Position(position p)
        {
            var db = new DatabaseEntities();
            return new Common.Model.Position
            {
                Player = (Common.Model.Player) p.member,
                IsExtra = p.IsExtra
            };
        }
    }
}
