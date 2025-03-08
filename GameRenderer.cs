using Silk.NET.SDL;

namespace TheAdventure;

public unsafe class GameRenderer
{
    private Sdl _sdl;
    private IntPtr _renderer;

    public GameRenderer(Sdl sdl, GameWindow gameWindow, GameLogic gameLogic)
    {
        _sdl = sdl;
        _renderer = gameWindow.CreateRenderer();
    }

    public void Render()
    {
        _sdl.RenderPresent((Renderer*)_renderer);
    }
}