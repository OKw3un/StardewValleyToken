using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsInput;

// This function brings a window to the foreground, making it the active window.
// It is part of the User32.dll library, so we use DllImport to access it.
[DllImport("User32.dll")]                            
static extern int SetForegroundWindow(IntPtr point);

// Gets the currently active window
[DllImport("User32.dll")]
static extern IntPtr GetForegroundWindow(); 

// The Process class is used to start, stop, and manage system processes.
// This retrieves all running processes with the name "Stardew Valley".
Process[] ps = Process.GetProcessesByName("Stardew Valley");

// FirstOrDefault() returns the first element in the array or null if not found.
Process stardewValleyProcess = ps.FirstOrDefault();

// Display the retrieved process information.
Console.WriteLine(stardewValleyProcess);

if(stardewValleyProcess != null)
{
    Console.WriteLine("Please enter the desired width: ");
    int width = int.Parse(Console.ReadLine());
    Console.WriteLine("Please enter the desired length: ");
    int length = int.Parse(Console.ReadLine());

    Console.WriteLine("Bringing Stardew Valley on focus...");

    // IntPtr (Integer Pointer) represents a platform-independent pointer.
    // MainWindowHandle is a handle that represents the main application window.
    IntPtr mainApplication = stardewValleyProcess.MainWindowHandle;

    // Bring the application window to the top and make it the active one.
    SetForegroundWindow(mainApplication);
    // 27-30 are required to give the focus to the application.

    // By waiting 1 second, program ensures that the game window actually come to the foreground.
    Thread.Sleep(1000);
    Console.WriteLine("Getting Stardew Valley out of Game Menu");

    // Variable to imitate keyboard and mouse inputs.
    InputSimulator mainApplicationVirtualKeyboardMouse = new InputSimulator();

    // Pressing escape bar to quit the game menu.
    mainApplicationVirtualKeyboardMouse.Keyboard.KeyPress (WindowsInput.Native.VirtualKeyCode.ESCAPE);

    // Bring the application window to the top and make it the active one.
    SetForegroundWindow(mainApplication);

    Console.WriteLine("Please adjust your mouse cursor.(waiting for 5 second.)");
    Thread.Sleep(5000);
    FarmAutomation(mainApplicationVirtualKeyboardMouse, width, length, mainApplication);

}

Console.WriteLine("Press a key to exit...");
Console.ReadKey();

static void FarmAutomation(InputSimulator keyboardAndMouse, int width, int length, IntPtr app)
{
    // Brings the target application to the foreground
    SetForegroundWindow(app);

    while (length > 0) // Loop to control movement for the given length
    {
        // If the target window is active, stop execution
        if (IsWindowActive(app))
        {
            break;
        }

        // Move downward for (width - 1) steps
        for (int j = 0; j < width - 1; j++)
        {
            if (IsWindowActive(app)) // Check if the window is active
            {
                break;
            }

            ControlMouse(keyboardAndMouse); // Perform an action with the mouse
            VerticalMovementDownside(keyboardAndMouse); // ⬇️ Move downward
        }

        length--; // Reduce the remaining length
        ControlMouse(keyboardAndMouse); // Perform an action with the mouse

        if (length == 0) // If no more length left, stop execution
        {
            break;
        }

        HorizontalMovementLeft(keyboardAndMouse); // Move left

        // Move upward for (width - 1) steps
        for (int k = 0; k < width - 1; k++)
        {
            if (IsWindowActive(app)) // Check if the window is active
            {
                break;
            }

            ControlMouse(keyboardAndMouse); // Perform an action with the mouse
            VerticalMovementUpside(keyboardAndMouse); // ⬆️ Move upward
        }

        length--; // Reduce the remaining length
        ControlMouse(keyboardAndMouse); // Perform an action with the mouse
        HorizontalMovementLeft(keyboardAndMouse); // Move left
    }
}

// Moves the player horizontally to the left
static void HorizontalMovementLeft(InputSimulator keyboard)
{
    Thread.Sleep(500); // Wait before movement
    keyboard.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_A); // Press 'A' to move left
    Thread.Sleep(195); // Hold 'A' for a short duration
    keyboard.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_A); // Release 'A'
}

// Moves the player vertically downward with slight adjustments
static void VerticalMovementDownside(InputSimulator keyboard)
{
    Thread.Sleep(500); // Wait before movement
    keyboard.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_S); // Press 'S' to move down
    Thread.Sleep(195); // Hold 'S' for a short time
    keyboard.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_S); // Release 'S'

    Thread.Sleep(1); // Short delay
    keyboard.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_D); // Press 'D' (small right adjustment)
    Thread.Sleep(1);
    keyboard.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_D); // Release 'D'

    Thread.Sleep(1); // Short delay
    keyboard.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_A); // Press 'A' (small left adjustment)
    Thread.Sleep(1);
    keyboard.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_A); // Release 'A'
}

// Moves the player vertically downward with slight adjustments
static void VerticalMovementUpside(InputSimulator keyboard)
{
    // Moves the player vertically upward with slight adjustments
    Thread.Sleep(500); // Wait before movement
    keyboard.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_W); // Press 'W' to move up
    Thread.Sleep(195); // Hold 'W' for a short time
    keyboard.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_W); // Release 'W'

    Thread.Sleep(1); // Short delay
    keyboard.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_D); // Press 'D' (small right adjustment)
    Thread.Sleep(1);
    keyboard.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_D); // Release 'D'

    Thread.Sleep(1); // Short delay
    keyboard.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_A); // Press 'A' (small left adjustment)
    Thread.Sleep(1);
    keyboard.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_A); // Release 'A'
}

static void ControlMouse(InputSimulator mouse)
{
    Thread.Sleep(500); // Wait before actions begin

    // Using a hoe to till the soil
    Console.WriteLine("Tilling the soil with a hoe.");
    mouse.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_6); // Select hoe (key '6')
    Thread.Sleep(200);
    mouse.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_6);

    mouse.Mouse.LeftButtonDown(); // Press left mouse button (hoe action)
    Thread.Sleep(50);
    mouse.Mouse.LeftButtonUp(); // Release left mouse button

    Thread.Sleep(500); // Wait before the next action
    
    // Sowing seeds
    Console.WriteLine("Seeds are being sown.");
    mouse.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_7); // Select seeds (key '7')
    Thread.Sleep(200);
    mouse.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_7);

    mouse.Mouse.RightButtonDown(); // Press right mouse button (sow seeds)
    Thread.Sleep(50);
    mouse.Mouse.RightButtonUp(); // Release right mouse button
}

// Method for checking the active window
static bool IsWindowActive(IntPtr app)
{

    // Check if Stardew Valley is still the active window
    if (GetForegroundWindow() != app)
    {
        Console.WriteLine("Stardew Valley is not in focus! Stopping input.");
        return true; // Stop execution if another window is focused
    }
    return false;
} 