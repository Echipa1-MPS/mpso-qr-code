using Microcharts;
using QR_Presence.Models.APIModels;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;

namespace QR_Presence.Services
{
    public static class StatsService
    {
        public static List<string> Summary = new List<string>
        {
            "Pr",
            "Att",
            "Act"
        };

        public static StatsModel Stats { get; set; }

        public static async Task<Chart> GenerateStatsForDate(string date, int id)
        {
            Stats = await Services.APICalls.GetStatsForDate(date, id);
            Color color1 = (Color)Application.Current.Resources["ChartColor6"];

            List<Entry> Entrys = new List<Entry>();
            string label;

            for (int i = 0; i < Stats.List_qr_attendance.Count; i++)
            {
                try
                {
                    label = $"{Summary[i]}{i}";
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    label = $"QR{i}";
                }

                Entrys.Add(new Entry(Stats.List_qr_attendance[i].Count)
                {
                    Color = SKColor.Parse(color1.ToHex()),
                    ValueLabelColor = SKColor.Parse(color1.ToHex()),
                    Label = label,
                    TextColor = SKColor.Parse(color1.ToHex()),
                    ValueLabel = $"{Stats.List_qr_attendance[i].Count}"
                });
            }

            Entrys.Add(new Entry(Stats.Absent)
            {
                Color = SKColor.Parse(color1.ToHex()),
                ValueLabelColor = SKColor.Parse(color1.ToHex()),
                Label = "Abs",
                TextColor = SKColor.Parse(color1.ToHex()),
                ValueLabel = $"{Stats.Absent}"
            });
            Entrys.Add(new Entry(Stats.FullStrike)
            {
                Color = SKColor.Parse(color1.ToHex()),
                ValueLabelColor = SKColor.Parse(color1.ToHex()),
                Label = "FStk",
                TextColor = SKColor.Parse(color1.ToHex()),
                ValueLabel = $"{Stats.FullStrike}"
            });

            return new BarChart
            {
                Entries = Entrys,
                LabelTextSize = 40f,
                Margin = 50,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                BackgroundColor = SKColor.Parse(Color.Transparent.ToHex())
            };
        }
    }
}
