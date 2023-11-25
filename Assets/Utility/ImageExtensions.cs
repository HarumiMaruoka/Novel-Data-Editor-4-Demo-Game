// 日本語対応
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class ImageExtensions
{
    public static async UniTask FadeInAsync(this Image image, float duration)
    {
        var from = image.color;
        from.a = 0;

        var to = image.color;
        to.a = 1;
        await FadeAsync(image, from, to, duration);
    }

    public static async UniTask FadeOutAsync(this Image image, float duration)
    {
        var from = image.color;
        from.a = 1;

        var to = image.color;
        to.a = 0;
        await FadeAsync(image, from, to, duration);
    }

    public static async UniTask FadeAsync(this Image image, Color from, Color to, float duration)
    {
        for (var t = 0F; t < duration; t += Time.deltaTime)
        {
            image.color = Color.Lerp(from, to, t / duration);
            await UniTask.Yield();
        }
    }
}