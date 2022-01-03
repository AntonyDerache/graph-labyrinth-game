public interface IScene
{
    Actions GetInput();
    void Compute(Actions action);
    void Draw();
    void Help();
};