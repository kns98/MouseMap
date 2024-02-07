using System;
using System.Windows.Forms;
using System.Diagnostics; // For Stopwatch

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        private Point lastMousePosition;
        private Stopwatch stopwatch;
        private double lastSpeed = 0.0;

        public MainForm()
        {
            InitializeComponent();
            this.MouseMove += MainForm_MouseMove;
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!lastMousePosition.IsEmpty)
            {
                // Calculate time elapsed since last movement
                double timeElapsed = stopwatch.ElapsedMilliseconds / 1000.0; // Convert to seconds
                stopwatch.Restart();

                // Calculate distance moved since last event
                double distance = Math.Sqrt(Math.Pow(e.X - lastMousePosition.X, 2) + Math.Pow(e.Y - lastMousePosition.Y, 2));

                // Calculate current speed (distance/time)
                double currentSpeed = distance / timeElapsed;

                // Calculate acceleration (change in speed / time)
                double acceleration = (currentSpeed - lastSpeed) / timeElapsed;

                // Update lastSpeed for the next calculation
                lastSpeed = currentSpeed;

                // Map the acceleration to a range (for demonstration, we use an arbitrary mapping)
                // Note: You might want to establish a suitable range for acceleration values based on observation
                int generatedNumber = MapRange((int)Math.Abs(acceleration), 0, 50, 0, 100);

                // Display the generated number (based on acceleration) in the form's title
                this.Text = $"Generated Number (Based on Acceleration): {generatedNumber}";
            }

            lastMousePosition = e.Location;
        }

        private int MapRange(int value, int fromSource, int toSource, int fromTarget, int toTarget)
        {
            return (value - fromSource) * (toTarget - fromTarget) / (toSource - fromSource) + fromTarget;
        }
    }
}
