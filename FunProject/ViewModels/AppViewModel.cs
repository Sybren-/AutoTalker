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

        private readonly Dictionary<string, string> _textEffects = new Dictionary<string, string>()
        {
            {"Flash 1 (Text cycles red to yellow quickly)", "flash1:"},
            {"Flash 2 (Text cycles blue to cyan quickly)", "flash2:"},
            {"Flash 3 (Text cycles green to light green quickly)", "flash3:"},
            {"Glow 1 (Text fades red to blue)", "glow1:"},
            {"Glow 2 (Text fades red to purple to blue)", "glow2:"},
            {"Glow 3 (Text fades white to green to blue)", "glow3:" },
            {"Scroll (Text scrolls right to left)", "scroll:" },
            {"Shake (Text shakes up and down)", "shake:" },
            {"Slide (Text slides from top to bottom)", "slide:" },
            {"Wave (Text waves up and down)", "wave:" },
            {"Wave 2 (Text waves slowly from left to right)", "wave2:" }
        };


        public Dictionary<string, string> TextEffects
        {
            get { return _textEffects; }
        }

        private ObservableCollection<KeyValuePair<string, string>> _textEffectCodes = new ObservableCollection<KeyValuePair<string, string>>();
        public ObservableCollection<KeyValuePair<string, string>> TextEffectCodes
        {
            get { return _textEffectCodes; }
            set
            {
                _textEffectCodes = value;
                NotifyOfPropertyChange(() => TextEffectCodes);
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

            var x = TextEffectCodes;
            var message = TextEffectsProcessor.ProcessMessageTextColor(Message, TextColorCode, "TIJDELIJK");

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
