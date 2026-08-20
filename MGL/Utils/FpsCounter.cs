namespace MGL.Utils;

public class FpsCounter
{
    public int FPS { get; private set; } = 0;
    
    private double _timer = 0.0;
    private int _frames = 0;

    public void Frame(double deltaTime)
    {
        _frames++;
        _timer += deltaTime;
        
        if (_timer >= 1.0)
        {
            FPS = _frames;    
            _frames = 0;      
            _timer -= 1.0;       
        }
    }
}