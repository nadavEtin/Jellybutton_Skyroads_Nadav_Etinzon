public interface IUnityCoreService
{
    void RegisterToUpdate(IUpdate method);

    void RegisterToStart(IStart method);
}
