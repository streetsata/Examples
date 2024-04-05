namespace DataAccessLibrary
{
    public interface IDelayService
    {
        Task Delay(int milliseconds);
    }

    public class DelayService : IDelayService
    {
        public async Task Delay(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
    }
}