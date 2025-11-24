using System;
using System.Collections.Generic;
using System.Text;

namespace RedFocus.Model;
public class TimerConfig
{
    public TimeSpan FocusTime { get; set; }
    public TimeSpan ShortBreakTime { get; set; }
    public TimeSpan LongBreakTime { get; set; }
    public int Rounds { get; set; } = 0;
}
