namespace Blocks.Redis
{
    using global::Redis.OM.Modeling;

    public class Entity
    {
        [RedisIdField]
        [Indexed]
        public int Id { get; set; }
    }
}
