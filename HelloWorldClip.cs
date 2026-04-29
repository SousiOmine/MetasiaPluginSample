using Metasia.Core.Attributes;
using Metasia.Core.Objects;
using Metasia.Core.Objects.VisualEffects;
using Metasia.Core.Render;
using SkiaSharp;

namespace MetasiaPluginSample;

// この属性をつけないと、誰もクリップとは認めてくれない
[ClipTypeIdentifier("HelloWorldClip", FallbackText = "ハローワールド")]
public class HelloWorldClip : ClipObject, IRenderable
{
    public List<VisualEffectBase> VisualEffects { get; set; } = new();

    // プロジェクト読み込み時のデシリアライズのために、パラメータなしのコンストラクタが絶対に必要なので注意！
    public HelloWorldClip()
    {

    }

    public Task<IRenderNode> RenderAsync(RenderContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // 返したい画像の論理サイズ
        int logicalWidth = 800;
        int logicalHeight = 400;
        
        float renderScaleWidth = context.RenderResolution.Width / context.ProjectResolution.Width;
        float renderScaleHeight = context.RenderResolution.Height / context.ProjectResolution.Height;

        // 実際に返す画像のサイズを求める
        int imageWidth = (int)(logicalWidth * renderScaleWidth);
        int imageHeight = (int)(logicalHeight * renderScaleHeight);

        var info = new SKImageInfo(imageWidth, imageHeight, SKColorType.Rgba8888, SKAlphaType.Premul);
        using var surface = context.SurfaceFactory.CreateSurface(info);
        using var canvas = surface.Canvas;

        canvas.Scale(renderScaleWidth, renderScaleHeight);

        SKPaint skPaint = new()
        {
            IsAntialias = true,
            Color = SKColors.Yellow
        };

        canvas.DrawText("Hello World from Plugin!", new SKPoint(100, 100), SKTextAlign.Center, new SKFont() { Size = 32 }, skPaint);

        var image = context.SurfaceFactory.Snapshot(surface, context.PreferRasterOutput);

        return Task.FromResult<IRenderNode>(new NormalRenderNode()
        {
            Image = image,
            LogicalSize = new SKSize(logicalWidth, logicalHeight),
        });
    }
}