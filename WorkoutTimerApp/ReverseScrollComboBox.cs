using System;
using System.Windows.Forms;

namespace WorkoutTimerApp
{
    // ComboBox where scroll up = higher index (longer duration), scroll down = lower index (shorter duration)
    public class ReverseScrollComboBox : ComboBox
    {
        private const int WM_MOUSEWHEEL = 0x020A;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL && Items.Count > 0)
            {
                short delta = (short)(m.WParam.ToInt64() >> 16);
                int step = delta > 0 ? 1 : -1;
                int current = SelectedIndex < 0 ? 0 : SelectedIndex;
                int newIndex = Math.Max(0, Math.Min(Items.Count - 1, current + step));
                if (newIndex != SelectedIndex)
                    SelectedIndex = newIndex;
                return;
            }
            base.WndProc(ref m);
        }
    }
}
