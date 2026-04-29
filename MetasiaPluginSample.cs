using Metasia.Editor.Abstractions.Hosting;
using Metasia.Editor.Plugin;

namespace MetasiaPluginSample;

public class MetasiaPluginSample : IClipTypeProvider
{
    public string PluginIdentifier => "com.SousiOmine.MetasiaPluginSample";

    public string PluginVersion => "1.0.0";

    public string PluginName => "サンプルプラグイン";

    public IEnumerable<IEditorPlugin.SupportEnvironment> SupportedEnvironments => [
        IEditorPlugin.SupportEnvironment.Windows_x64,
        IEditorPlugin.SupportEnvironment.MacOS_arm64,
        IEditorPlugin.SupportEnvironment.Linux_x64
    ];

    public IEnumerable<Type> GetClipTypes()
    {
        yield return typeof(HelloWorldClip);
    }

    public void Initialize(IEditorHostContext hostContext)
    {
        Console.WriteLine("サンプルプラグインが読み込まれました！");
    }
}