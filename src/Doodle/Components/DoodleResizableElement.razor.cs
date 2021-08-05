using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Doodle.Components
{

    public partial class DoodleResizableElement : ComponentBase
    { 

        #region Members 
        private Abstractions.Config.DoodleDrawConfig _config;
        private Abstractions.Config.DoodleDrawConfig _options;

        [Inject]
        private Abstractions.JsInterop.IJsInteropDragDrop JsInteropDragDrop { get; set;}

        [Inject]
        private ILogger<DoodleResizableElement> Logger { get; set; }

        [Inject]
        private Abstractions.Config.DoodleDrawConfig Config 
        { 
            get => _config; 
            set
            {
                if (_config != value)
                {
                    _config = value;
                    InitConfigSettings(value);
                }
            } 
        }

        private ElementReference ResizeElement { get; set; }

        private bool _elementActive = false;

        private Abstractions.Models.ElementDimensions _currentDimensions = new Abstractions.Models.ElementDimensions();

        public bool ElementInitialised { get; private set; } = false;

        private bool _allowMove = true;

        private bool _allowResize = true;

        private bool _autoHandleEvents = true;
        #endregion

        #region Events
        [Parameter]
        public EventCallback<DoodleResizableElement> ResizeElementReady { get; set; }

        [Parameter]
        public EventCallback<double?> MinWidthChanged { get; set; }

        [Parameter]
        public EventCallback<double?> MinHeightChanged { get; set; }

        [Parameter]
        public EventCallback<double> LeftChanged { get; set; }

        [Parameter]
        public EventCallback<bool> ElementActiveChanged { get; set; }

        [Parameter]
        public EventCallback<double> HeightChanged { get; set; }

        [Parameter]
        public EventCallback<double> WidthChanged { get; set; }

        [Parameter]
        public EventCallback<double> TopChanged { get; set; }

        [Parameter]
        public EventCallback<Abstractions.Models.ElementDimensions> DimensionsChanged { get; set; }

        [Parameter]
        public EventCallback<bool> AutoHandleEventsChanged { get; set; }
        #endregion

        #region Properties
        [Parameter]
        public string ResizeAdornerClass { get; set; }

        [Parameter]
        public RenderFragment<DoodleResizableElement> InActiveContent { get; set; }

        [Parameter]
        public RenderFragment<DoodleResizableElement> ActiveContent { get; set; }

        [Parameter]
        public RenderFragment<DoodleResizableElement> ChildContent { get; set; }

        [Parameter]
        public bool AutoHandleEvents 
        { 
            get => _autoHandleEvents; 
            set
            {
                if (_autoHandleEvents != value)
                {
                    _autoHandleEvents = value;
                    this.AutoHandleEventsChanged.InvokeAsync(_autoHandleEvents);
                    this.SetAutoHandleEvents().ConfigureAwait(false);
                }
            } 
        }

        [Parameter]
        public bool ElementActive 
        { 
            get => _elementActive; 
            set 
            {
                if (_elementActive != value)
                {
                    _elementActive = value;
                    ElementActiveChanged.InvokeAsync(_elementActive);
                    UpdateElementActive().ConfigureAwait(false); 
                }
            } 
        }

        [Parameter]
        public double Height 
        { 
            get => _currentDimensions.Height; 
            set
            {
                if (_currentDimensions.Height != value)
                {
                    _currentDimensions.Height = value;
                    this.HeightChanged.InvokeAsync(_currentDimensions.Height);
                    this.DimensionsChanged.InvokeAsync(_currentDimensions);
                }
            } 
        } 

        [Parameter]
        public double Width
        { 
            get => _currentDimensions.Width; 
            set
            {
                if (_currentDimensions.Width != value)
                {
                    _currentDimensions.Width = value;
                    this.WidthChanged.InvokeAsync(_currentDimensions.Width);
                    this.DimensionsChanged.InvokeAsync(_currentDimensions);
                }
            } 
        } 

        [Parameter]
        public double Top 
        { 
            get => _currentDimensions.Top; 
            set
            {
                if (_currentDimensions.Top != value)
                {
                    _currentDimensions.Top = value;
                    this.TopChanged.InvokeAsync(_currentDimensions.Top);
                    this.DimensionsChanged.InvokeAsync(_currentDimensions);
                }
            } 
        } 

        [Parameter]
        public double Left
        { 
            get => _currentDimensions.Left; 
            set
            {
                if (_currentDimensions.Left != value)
                {
                    _currentDimensions.Left = value;
                    this.LeftChanged.InvokeAsync(_currentDimensions.Left);
                    this.DimensionsChanged.InvokeAsync(_currentDimensions);
                }
            } 
        } 

        [Parameter]
        public double? MinWidth 
        { 
            get => _currentDimensions.MinWidth; 
            set
            {
                if (_currentDimensions.MinWidth != value) 
                {
                    _currentDimensions.MinWidth = value;
                    this.MinWidthChanged.InvokeAsync(_currentDimensions.MinWidth);
                    this.DimensionsChanged.InvokeAsync(_currentDimensions);
                }
            } 
        }

        [Parameter]
        public double? MinHeight 
        { 
            get => _currentDimensions.MinHeight; 
            set
            {
                if (_currentDimensions.MinHeight != value) 
                {
                    _currentDimensions.MinHeight = value;
                    this.MinHeightChanged.InvokeAsync(_currentDimensions.MinHeight);
                    this.DimensionsChanged.InvokeAsync(_currentDimensions);
                }
            } 
        }
      
        [Parameter]
        public bool AllowResize 
        { 
            get => _allowResize; 
            set
            {
                if (_allowResize != value)
                {
                    _allowResize = value;
                    this.SetAllowResize().ConfigureAwait(false);
                }
            } 
        }

        [Parameter]
        public bool AllowMove
        { 
            get => _allowMove; 
            set
            {
                if (_allowMove != value)
                {
                    _allowMove = value;
                    this.SetAllowMove().ConfigureAwait(false);
                }
            } 
        }

        [Parameter]
        public string ResizeElementClass { get; set; }

        [Parameter]
        public string MoveAdornerClass { get; set; }

        [Parameter]
        public Abstractions.Config.DoodleDrawConfig Options 
        { 
            get => _options; 
            set 
            {
                if (_options != value)
                {
                    _options = value;
                    InitConfigSettings(value);
                }
            } 
        }
        #endregion

        #region Config Init
        private void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null || config.ResizableElementConfig == null) return;

            this.AllowMove = config.ResizableElementConfig.AllowMove;
            this.AllowResize = config.ResizableElementConfig.AllowResize;
            this.AutoHandleEvents = config.ResizableElementConfig.AutoHandleEvents;
            this.Height = config.ResizableElementConfig.Height;
            this.Width = config.ResizableElementConfig.Width;
            this.Top = config.ResizableElementConfig.Top;
            this.Left = config.ResizableElementConfig.Left;
            this.MinWidth = config.ResizableElementConfig.MinWidth;
            this.MinHeight = config.ResizableElementConfig.MinHeight;
            this.ResizeElementClass = config.ResizableElementConfig.ResizeElementClass;
            this.MoveAdornerClass = config.ResizableElementConfig.MoveAdornerClass;
        }
        #endregion

        #region Overrides
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender == true && ElementInitialised == false)
            {
                Logger.LogDebug($"Initialising Resize component");
                await JsInteropDragDrop.InitialiseResizable(ResizeElement, this.AutoHandleEvents, this.ElementActive, this.AllowResize, this.AllowMove, this.MinWidth, this.MinHeight);

                this.JsInteropDragDrop.OnElementUpdated += JsInteropDragDrop_OnElementUpdated;
                this.JsInteropDragDrop.OnSetIsActive += JsInteropDragDrop_OnSetIsActive;
                
                Logger.LogDebug($"Resize component Initialised");
                this.ElementInitialised = true;
                await ResizeElementReady.InvokeAsync(this);
            }
        }

        private async Task SetAllowMove()
        {
            if (this.ElementInitialised)
            {
                await this.JsInteropDragDrop.SetAllowMove(this._allowMove);
            }
        }

        private async Task SetAllowResize()
        {
            if (this.ElementInitialised)
            {
                await this.JsInteropDragDrop.SetAllowResize(this._allowResize);
            }
        }

        private async Task SetMinWidth()
        {
            if (this.ElementInitialised)
            {
                await this.JsInteropDragDrop.SetMinWidth(this._currentDimensions.MinWidth);
            }
        }

        private async Task SetMinHeight()
        {
            if (this.ElementInitialised)
            {
                await this.JsInteropDragDrop.SetMinHeight(this._currentDimensions.MinHeight);
            }
        }

        private async Task UpdateElementActive()
        {
            if (this.ElementInitialised)
            {
                if (this.ElementActive)
                {
                    await this.JsInteropDragDrop.ActivateElement();
                }
                else
                {
                    await this.JsInteropDragDrop.DeActivateElement(); 
                }
            }
        }

        private async Task SetAutoHandleEvents()
        {
            if (this.ElementInitialised)
            {
                await this.JsInteropDragDrop.SetAutoHandleEvents(this.AutoHandleEvents);
            }
        }


        #endregion

        #region Event Handlers
        private void JsInteropDragDrop_OnSetIsActive(object sender, bool isActive)
        {
            Logger.LogDebug($"Resize element Updated");
            if (this.ElementActive != isActive)
            {
                this.ElementActive = isActive;
                StateHasChanged();
            }
        }

        private void JsInteropDragDrop_OnElementUpdated(object sender, Abstractions.Models.ElementDimensions dimensions)
        {
            Logger.LogDebug($"Resize element Updated");
            
            this.Height = dimensions.Height;
            this.Width = dimensions.Width;
            this.Left = dimensions.Left;
            this.Top = dimensions.Top;
            
        }
        #endregion

    }
}