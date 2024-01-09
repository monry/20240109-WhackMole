using VitalRouter;

namespace Monry.WhackMole.Commands;

public record struct ButtonClickedCommand(int Counter) : ICommand;
