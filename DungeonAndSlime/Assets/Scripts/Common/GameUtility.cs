using UnityEngine;

namespace Common
{
    public static class GameUtility
    {
        public static void SetScreenResolution(Camera camera)
        {
            var deviceWidth = Screen.width;
            var deviceHeight = Screen.height;

            Screen.SetResolution(
                (int)GameConfig.ScreenWidth,
                (int)(((float)deviceHeight / deviceWidth) * GameConfig.ScreenWidth),
                true);

            if (GameConfig.ScreenWidth / GameConfig.ScreenHeight < (float)deviceWidth / deviceHeight) 
            {
                var newWidth = ((float)GameConfig.ScreenWidth / GameConfig.ScreenHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
                camera.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
            }
            else 
            {
                var newHeight = ((float)deviceWidth / deviceHeight) / ((float)GameConfig.ScreenWidth / GameConfig.ScreenHeight); // 새로운 높이
                camera.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
            }
        }
    }
}