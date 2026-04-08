namespace MinecraftIDChanger;

static class Program
{
    [STAThread]
    static void Main()
    {
        // .NET 6+ recommended bootstrap: sets DPI awareness,
        // visual styles, and text rendering in one call.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}
