namespace HouseHub.Web.UserApi.Core.Rabbit.Helpers.Setup
{
    public class QueueSetup
    {
        public string QueueName { get; set; }
        public ushort Qos { get; set; }
    }
}