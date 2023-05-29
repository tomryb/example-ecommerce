namespace Backend;

public interface IModule
{
    ApiEndpoint[] AddModule();
}

public interface IModule<T>
{
    ApiEndpoint[] AddModule(T dependency);
}

public interface IModule<T1, T2>
{
    ApiEndpoint[] AddModule(T1 dependency1, T2 dependency2);
}

public interface IModule<T1, T2, T3>
{
    ApiEndpoint[] AddModule(T1 dependency1, T2 dependency2, T3 dependency3);
}