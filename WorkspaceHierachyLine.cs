using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace ListBoxExample
{
    public class WorkspaceHierachyLine : Panel
    {
        public static readonly StyledProperty<bool> IsLastChildProperty =
            AvaloniaProperty.Register<WorkspaceHierachyLine, bool>(nameof(IsLastChild));

        public bool IsLastChild
        {
            get => GetValue(IsLastChildProperty);
            set => SetValue(IsLastChildProperty, value);
        }

        static WorkspaceHierachyLine()
        {
            AffectsRender<WorkspaceHierachyLine>(IsLastChildProperty);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            Pen pen = new Pen(Brushes.Black, 2);

            context.DrawLine(pen, new Point(Bounds.Width / 2, 0), new Point(Bounds.Width / 2, Bounds.Height / 2));
            context.DrawLine(pen, new Point(Bounds.Width / 2, Bounds.Height / 2), new Point(Bounds.Width, Bounds.Height / 2));

            if (IsLastChild)
                return;

            context.DrawLine(pen, new Point(Bounds.Width / 2, Bounds.Height / 2), new Point(Bounds.Width / 2, Bounds.Height));
        }
    }
}
