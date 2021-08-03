using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Doodle.Components
{

    public partial class DoodleResizable : ComponentBase
    {

        #region Members
        [Inject]
        private Abstractions.JsInterop.IJsInteropDragDrop JsInteropDragDrop { get; set;}

        [Inject]
        private ILogger<DoodleResizable> Logger { get; set; }

        private ElementReference ResizeElement { get; set; }

        private bool _resizeActive = false;
        private double _left = 0;
        private double _top = 0;
        private double _width = 100;
        private double _height = 20;
        private bool _interopUpdate = false;
        #endregion

        #region Events
        public EventCallback<DoodleResizable> ResizeElementReady { get; set; }
        #endregion

        #region Properties

        [Parameter]
        public RenderFragment<DoodleResizable> InActiveContent { get; set; }

        [Parameter]
        public RenderFragment<DoodleResizable> ActiveContent { get; set; }

        [Parameter]
        public RenderFragment<DoodleResizable> ChildContent { get; set; }

        [Parameter]
        public bool AutoHandleEvents { get; set; } = true;

        [Parameter]
        public bool ResizeActive 
        { 
            get => _resizeActive; 
            set 
            {
                if (_resizeActive != value)
                {
                    _resizeActive = value;
                    ResizeActiveChanged.InvokeAsync(_resizeActive);
                    UpdateResizeActive().ConfigureAwait(false);
                }
            } 
        }

        [Parameter]
        public EventCallback<bool> ResizeActiveChanged { get; set; }

        [Parameter]
        public double Height 
        { 
            get => _height; 
            set
            {
                if (_height != value)
                {
                    _height = value;
                    this.HeightChanged.InvokeAsync(_height);
                }
            } 
        } 

        [Parameter]
        public EventCallback<double> HeightChanged { get; set; }

        [Parameter]
        public double Width
        { 
            get => _width; 
            set
            {
                if (_width != value)
                {
                    _width = value;
                    this.WidthChanged.InvokeAsync(_width);
                }
            } 
        } 

        [Parameter]
        public EventCallback<double> WidthChanged { get; set; }

        [Parameter]
        public double Top 
        { 
            get => _top; 
            set
            {
                if (_top != value)
                {
                    _top = value;
                    this.TopChanged.InvokeAsync(_top);
                }
            } 
        } 

        [Parameter]
        public EventCallback<double> TopChanged { get; set; }

        [Parameter]
        public double Left
        { 
            get => _left; 
            set
            {
                if (_left != value)
                {
                    _left = value;
                    this.LeftChanged.InvokeAsync(_left);
                }
            } 
        } 

        [Parameter]
        public EventCallback<double> LeftChanged { get; set; }

        public bool ElementInitialised { get; private set; } = false;
        #endregion

        #region Overrides
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender == true && ElementInitialised == false)
            {
                Logger.LogDebug($"Initialising Resize component");
                await JsInteropDragDrop.InitialiseResizable(ResizeElement, new Abstractions.Models.ElementDimensions(this.Top, this.Left, this.Width, this.Height));

                this.JsInteropDragDrop.OnElementMoved += JsInteropDragDrop_OnElementMoved;
                this.JsInteropDragDrop.OnElementResized += JsInteropDragDrop_OnElementResized;
                this.JsInteropDragDrop.OnElementUpdated += JsInteropDragDrop_OnElementUpdated;
                
                Logger.LogDebug($"Resize component Initialised");
                this.ElementInitialised = true;
                await ResizeElementReady.InvokeAsync(this);
            }
        }
        #endregion

        #region Event Handlers
        private async Task UpdateResizeActive()
        {
            if (this.ElementInitialised)
            {
                if (this.ResizeActive)
                {
                    await this.JsInteropDragDrop.ActivateElement();
                }
                else
                {
                    await this.JsInteropDragDrop.DeActivateElement();
                }
            }
        }

        private void JsInteropDragDrop_OnElementMoved(object sender, Abstractions.Models.ElementDimensions dimensions)
        {
            Logger.LogDebug($"Resize element Moved");
            this._interopUpdate = true;
            this.Left = dimensions.Left;
            this.Top = dimensions.Top;
            this._interopUpdate = false;
        }

        private void JsInteropDragDrop_OnElementResized(object sender, Abstractions.Models.ElementDimensions dimensions)
        {
            Logger.LogDebug($"Resize element Resized");
            this._interopUpdate = true;
            this.Height = dimensions.Height;
            this.Width = dimensions.Width;
            this._interopUpdate = false;
        }

        private void JsInteropDragDrop_OnElementUpdated(object sender, Abstractions.Models.ElementDimensions dimensions)
        {
            Logger.LogDebug($"Resize element Updated");
            this._interopUpdate = true;
            this.Height = dimensions.Height;
            this.Width = dimensions.Width;
            this.Left = dimensions.Left;
            this.Top = dimensions.Top;
            this._interopUpdate = false;
        }
        #endregion

    }


}