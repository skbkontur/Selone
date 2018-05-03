using System.Drawing;
using OpenQA.Selenium;

namespace Kontur.Selone.Extensions
{
    public static class WindowExtensions
    {
        public static void SetSize(this IWindow window, int? width, int? height)
        {
            if (width == null && height == null)
            {
                return;
            }

            window.Size = new Size {Width = width ?? window.Size.Width, Height = height ?? window.Size.Height};
        }

        public static void SetWidth(this IWindow window, int width)
        {
            window.Size = new Size {Width = width, Height = window.Size.Height};
        }

        public static void SetHeight(this IWindow window, int height)
        {
            window.Size = new Size {Width = window.Size.Width, Height = height};
        }
    }
}