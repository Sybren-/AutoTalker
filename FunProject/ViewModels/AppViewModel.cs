using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace FunProject.ViewModels
{
    class AppViewModel : PropertyChangedBase
    {

        private InputSimulator _inputSimulator;
        private CancellationTokenSource _cancellationTokenSource;

        private string _windowTitle = "AutoTalker";
        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                NotifyOfPropertyChange(() => WindowTitle);
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        private int _interval = 1;
        public int Interval
        {
            get { return _interval; }
            set
            {
                _interval = value;
                NotifyOfPropertyChange(() => Interval);
            }
        }

        private bool _isRunning = false;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                NotifyOfPropertyChange(() => IsRunning);
            }
        }

        private readonly Dictionary<string, string> _textColors = new Dictionary<string, string>()
        {
            {"Cyan", "cyan:"},
            {"Green", "green:"},
            {"Purple", "purple:"},
            {"Red", "red:"},
            {"White", "white:"}
        };


        public Dictionary<string, string> TextColors
        {
            get { return _textColors; }
        }

        private string _textColorCode = string.Empty;
        public string TextColorCode
        {
            get { return _textColorCode; }
            set
            {
                _textColorCode = value;
                NotifyOfPropertyChange(() => TextColorCode);
            }
        }

        public readonly IEnumerable<TextEffect> _textEffects = new List<TextEffect>()
        {
            new TextEffect { Name = "Flash 1", Code = "flash1:", Description = "Text cycles red to yellow quickly"},
            new TextEffect { Name = "Flash 2", Code = "flash2:", Description = "Text cycles blue to cyan quickly"},
            new TextEffect { Name = "Flash 3", Code = "flash3:", Description = "Text cycles green to light green quickly"},
            new TextEffect { Name = "Glow 1", Code = "glow1:", Description = "Text fades red to blue"},
            new TextEffect { Name = "Glow 2", Code = "glow2:", Description = "Text fades red to purple to blue"},
            new TextEffect { Name = "Glow 3", Code = "glow3:", Description = "Text fades white to green to blue"},
            new TextEffect { Name = "Scroll", Code = "scroll:", Description = "Text scrolls right to left"},
            new TextEffect { Name = "Shake", Code = "shake:", Description = "Text shakes up and down"},
            new TextEffect { Name = "Slide", Code = "slide:", Description = "Text slides from top to bottom"},
            new TextEffect { Name = "Wave", Code = "wave:", Description = "Text waves up and down"},
            new TextEffect { Name = "Wave2", Code = "wave2", Description = "Text waves slowly from left to right"}
        };


        public IEnumerable<TextEffect> TextEffects
        {
            get { return _textEffects; }
        }

        private ObservableCollection<TextEffect> _selectedTextEffects = new ObservableCollection<TextEffect>();
        public ObservableCollection<TextEffect> SelectedTextEffects
        {
            get { return _selectedTextEffects; }
            set
            {
                _selectedTextEffects = value;
                NotifyOfPropertyChange(() => SelectedTextEffects);
            }
        }

        public AppViewModel()
        {
            _inputSimulator = new InputSimulator();
        }

        public void Run()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () => await StartSimulatingInput(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
        }

        public async Task StartSimulatingInput(CancellationToken token)
        {
            IsRunning = true;

            var message = TextEffectsProcessor.ProcessMessageTextColor(Message, TextColorCode, SelectedTextEffects);

            while (!token.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(Interval));
                _inputSimulator.Keyboard.TextEntry(message);
                _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            }

            IsRunning = false;
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
