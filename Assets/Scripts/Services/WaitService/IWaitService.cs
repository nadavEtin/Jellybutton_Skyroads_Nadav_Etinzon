public interface IWaitService
{
    IAwaiter WaitFor(int waitDelay);
}
