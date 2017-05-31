using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Bshkara.Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AGridView = Android.Widget.GridView;
using Color = Android.Graphics.Color;
using GridView = XLabs.Forms.Controls.GridView;
using Orientation = Android.Content.Res.Orientation;
using PropertyChangingEventArgs = Xamarin.Forms.PropertyChangingEventArgs;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(GridView), typeof(GridViewRenderer))]

namespace Bshkara.Mobile.Droid.Renderers
{
    /// <summary>
    ///     Class GridViewRenderer.
    /// </summary>
    public class GridViewRenderer : ViewRenderer<GridView, AGridView>
    {
        /// <summary>
        ///     The orientation
        /// </summary>
        private readonly Orientation _orientation = Orientation.Undefined;

        /// <summary>
        ///     The <see cref="CellAdapter" /> implementation for <see cref="GridView" />
        /// </summary>
        private GridViewAdapter _adapter;

        /// <summary>
        ///     Called when the current configuration of the resources being used
        ///     by the application have changed.
        /// </summary>
        /// <param name="newConfig">The new resource configuration.</param>
        /// <since version="Added in API level 8" />
        /// <remarks>
        ///     <para tool="javadoc-to-mdoc">
        ///         Called when the current configuration of the resources being used
        ///         by the application have changed.  You can use this to decide when
        ///         to reload resources that can changed based on orientation and other
        ///         configuration characterstics.  You only need to use this if you are
        ///         not relying on the normal
        ///         <c>
        ///             <see cref="T:Android.App.Activity" />
        ///         </c>
        ///         mechanism of
        ///         recreating the activity instance upon a configuration change.
        ///     </para>
        ///     <para tool="javadoc-to-mdoc">
        ///         <format type="text/html">
        ///             <a
        ///                 href="http://developer.android.com/reference/android/view/View.html#onConfigurationChanged(android.content.res.Configuration)"
        ///                 target="_blank">
        ///                 [Android Documentation]
        ///             </a>
        ///         </format>
        ///     </para>
        /// </remarks>
        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (newConfig.Orientation != _orientation)
                OnElementChanged(new ElementChangedEventArgs<GridView>(Element, Element));
        }

        /// <summary>
        ///     Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<GridView> e)
        {
            base.OnElementChanged(e);

            var context = Forms.Context;
            var collectionView = new AGridView(context);
            collectionView.SetGravity(GravityFlags.Center);
            collectionView.SetColumnWidth(Convert.ToInt32(Element.ItemWidth));
            collectionView.StretchMode = StretchMode.StretchColumnWidth;

            var metrics = Resources.DisplayMetrics;
            var spacing = (int) e.NewElement.ColumnSpacing;
            var width = metrics.WidthPixels;
            var itemWidth = (int) e.NewElement.ItemWidth;

            var noOfColumns = width/(itemWidth + spacing);
            // If possible add another row without spacing (because the number of columns will be one less than the number of spacings)
            if (width - noOfColumns*(itemWidth + spacing) >= itemWidth)
                noOfColumns++;

            collectionView.SetNumColumns(noOfColumns);
            collectionView.SetPadding(Convert.ToInt32(Element.Padding.Left), Convert.ToInt32(Element.Padding.Top),
                Convert.ToInt32(Element.Padding.Right), Convert.ToInt32(Element.Padding.Bottom));

            collectionView.SetBackgroundColor(Element.BackgroundColor.ToAndroid());
            collectionView.SetHorizontalSpacing(Convert.ToInt32(Element.RowSpacing));
            collectionView.SetVerticalSpacing(Convert.ToInt32(Element.ColumnSpacing));

            Unbind(e.OldElement);
            Bind(e.NewElement);

            _adapter = new GridViewAdapter(context, collectionView, Element);
            collectionView.Adapter = _adapter;
            collectionView.ItemClick += CollectionViewItemClick;

            SetNativeControl(collectionView);
        }


        /// <summary>
        ///     Handles the ItemClick event of the collectionView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AdapterView.ItemClickEventArgs" /> instance containing the event data.</param>
        private void CollectionViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = Element.ItemsSource.Cast<object>().ElementAt(e.Position);
            Element.InvokeItemSelectedEvent(this, item);
        }

        /// <summary>
        ///     Unbinds the specified old element.
        /// </summary>
        /// <param name="oldElement">The old element.</param>
        private void Unbind(GridView oldElement)
        {
            if (oldElement != null)
            {
                oldElement.PropertyChanging -= ElementPropertyChanging;
                oldElement.PropertyChanged -= ElementPropertyChanged;
                if (oldElement.ItemsSource is INotifyCollectionChanged)
                    (oldElement.ItemsSource as INotifyCollectionChanged).CollectionChanged -= DataCollectionChanged;
            }
        }

        /// <summary>
        ///     Binds the specified new element.
        /// </summary>
        /// <param name="newElement">The new element.</param>
        private void Bind(GridView newElement)
        {
            if (newElement != null)
            {
                newElement.PropertyChanging += ElementPropertyChanging;
                newElement.PropertyChanged += ElementPropertyChanged;
                if (newElement.ItemsSource is INotifyCollectionChanged)
                    (newElement.ItemsSource as INotifyCollectionChanged).CollectionChanged += DataCollectionChanged;
            }
        }

        /// <summary>
        ///     Elements the property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs" /> instance containing the event data.</param>
        private void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GridView.ItemsSourceProperty.PropertyName)
                if (Element.ItemsSource is INotifyCollectionChanged)
                {
                    (Element.ItemsSource as INotifyCollectionChanged).CollectionChanged += DataCollectionChanged;
                    ReloadData();
                }
        }

        /// <summary>
        ///     Elements the property changing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangingEventArgs" /> instance containing the event data.</param>
        private void ElementPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == GridView.ItemsSourceProperty.PropertyName)
                if (Element.ItemsSource is INotifyCollectionChanged)
                    (Element.ItemsSource as INotifyCollectionChanged).CollectionChanged -= DataCollectionChanged;
        }

        /// <summary>
        ///     Datas the collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> instance containing
        ///     the event data.
        /// </param>
        private void DataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            if (_adapter != null)
                _adapter.NotifyDataSetChanged();
        }

        /// <summary>
        ///     Gets the cell.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="convertView">The convert view.</param>
        /// <param name="parent">The parent.</param>
        /// <returns>Android.Views.View.</returns>
        public View GetCell(int position, View convertView, ViewGroup parent)
        {
            var item = Element.ItemsSource.Cast<object>().ElementAt(position);
            var viewCellBinded = Element.ItemTemplate.CreateContent() as ViewCell;
            viewCellBinded.BindingContext = item;
            return CellFactory.GetCell(viewCellBinded, convertView, parent, Context, Element);
            //var view =  Xamarin.Forms.Platform.Android.Platform.CreateRenderer(viewCellBinded.View);
            //// Platform.SetRenderer (viewCellBinded.View, view);
            //view.ViewGroup.LayoutParameters = new  Android.Widget.GridView.LayoutParams (Convert.ToInt32(this.Element.ItemWidth), Convert.ToInt32(this.Element.ItemHeight));
            //view.ViewGroup.SetBackgroundColor (global::Android.Graphics.Color.Blue);
            //return view.ViewGroup;


            //                this.AddView (this.view.ViewGroup);

            //            GridViewCellRenderer render = new GridViewCellRenderer ();
            //           
            //            return render.GetCell (viewCellBinded, convertView, parent, this.Context);;
            //          //  view.LayoutParameters = new GridView.LayoutParams (this.Element.ItemWidth,this.Element.ItemHeight);
            //            return view;
            //            ImageView imageView;
            //
            //            if (convertView == null) {  // if it's not recycled, initialize some attributes
            //                imageView = new ImageView (Forms.Context);
            //                imageView.LayoutParameters = new GridView.LayoutParams (85, 85);
            //                imageView.SetScaleType (ImageView.ScaleType.CenterCrop);
            //                imageView.SetPadding (8, 8, 8, 8);
            //            } else {
            //                imageView = (ImageView)convertView;
            //            }
            //            var imageBitmap = GetImageBitmapFromUrl("http://xamarin.com/resources/design/home/devices.png");
            //            imageView.SetImageBitmap(imageBitmap);
            //            return imageView;
        }

        /// <summary>
        ///     Gets the image bitmap from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Bitmap.</returns>
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if ((imageBytes != null) && (imageBytes.Length > 0))
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            }

            return imageBitmap;
        }
    }

    /// <summary>
    ///     Class GridDataSource.
    /// </summary>
    public class GridViewAdapter : CellAdapter
    {
        /// <summary>
        ///     The native view
        /// </summary>
        private readonly AGridView _aView;

        /// <summary>
        ///     The _context
        /// </summary>
        private readonly Context _context;

        /// <summary>
        ///     The XF view
        /// </summary>
        private readonly GridView _view;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GridViewAdapter" /> class.
        /// </summary>
        /// <param name="context">The application context.</param>
        /// <param name="aView">The native Android view.</param>
        /// <param name="view">The Xamarin Forms view.</param>
        public GridViewAdapter(Context context, AGridView aView, GridView view) : base(context)
        {
            _context = context;
            _aView = aView;
            _view = view;
        }

        /// <summary>
        ///     Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public override int Count
        {
            get { return (_view.ItemsSource as ICollection).Count; }
        }

        /// <summary>
        ///     Gets the item at the given index.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override object this[int position]
        {
            get
            {
                var cell = GetCellForPosition(position);
                return cell;
            }
        }

        /// <summary>
        ///     Gets the item identifier.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>System.Int64.</returns>
        public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        ///     Gets the view.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="convertView">The convert view.</param>
        /// <param name="parent">The parent.</param>
        /// <returns>Android.Views.View.</returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var cell = GetCellForPosition(position);
            var renderer = new GridViewCellRenderer();
            var view = renderer.GetCell(cell, convertView, parent, _context);
            view.LayoutParameters = new AbsListView.LayoutParams(Convert.ToInt32(_view.ItemWidth),
                Convert.ToInt32(_view.ItemHeight));
#if DEBUG
            view.SetBackgroundColor(Color.Blue);
#endif
            view.SetPadding(Convert.ToInt32(_view.Padding.Left), Convert.ToInt32(_view.Padding.Top),
                Convert.ToInt32(_view.Padding.Right), Convert.ToInt32(_view.Padding.Bottom));
            return view;
        }

        protected override Cell GetCellForPosition(int position)
        {
            var item = _view.ItemsSource.Cast<object>().ElementAt(position);
            var cell = _view.ItemTemplate.CreateContent() as ViewCell;
            cell.BindingContext = item;

            return cell;
        }
    }

    /// <summary>
    ///     Class GridViewCellRenderer.
    /// </summary>
    public class GridViewCellRenderer : CellRenderer
    {
        /// <summary>
        ///     Gets the cell core.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="convertView">The convert view.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="context">The context.</param>
        /// <returns>Android.Views.View.</returns>
        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            var viewCell = (ViewCell) item;
            // TODO: implement reusing container!
            //var viewCellContainer = convertView as GridViewCellContainer;
            //if (viewCellContainer != null)
            //{
            //    // reuse existing container
            //    viewCellContainer.Update(viewCell);
            //    return viewCellContainer;
            //}

            var renderer = Platform.CreateRenderer(viewCell.View);
            //   Platform.SetRenderer (viewCell.View, renderer);
            // viewCell.View.IsPlatformEnabled = true;
            return new GridViewCellContainer(context, renderer, viewCell, parent);
        }

        /// <summary>
        ///     Class ViewCellContainer.
        /// </summary>
        private class GridViewCellContainer : ViewGroup
        {
            /// <summary>
            ///     The renderer
            /// </summary>
            private readonly IVisualElementRenderer _renderer;

            /// <summary>
            ///     The _parent
            /// </summary>
            private View _parent;

            /// <summary>
            ///     The _view cell
            /// </summary>
            private ViewCell _viewCell;

            /// <summary>
            ///     Initializes a new instance of the <see cref="GridViewCellContainer" /> class.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <param name="renderer">The view.</param>
            /// <param name="viewCell">The view cell.</param>
            /// <param name="parent">The parent.</param>
            public GridViewCellContainer(Context context, IVisualElementRenderer renderer, ViewCell viewCell,
                View parent) : base(context)
            {
                _renderer = renderer;
                _parent = parent;
                _viewCell = viewCell;
                AddView(renderer.ViewGroup);
            }

            /// <summary>
            ///     Updates the specified cell.
            /// </summary>
            /// <param name="cell">The cell.</param>
            public void Update(ViewCell cell)
            {
                var visualElementRenderer = GetChildAt(0) as IVisualElementRenderer;
                //              Type type = Registrar.Registered.GetHandlerType (cell.View.GetType ()) ?? typeof(RendererFactory.DefaultRenderer);
                //                if (visualElementRenderer != null && visualElementRenderer.GetType () == type) {
                //                    this.viewCell = cell;
                //                    visualElementRenderer.SetElement (cell.View);
                //                    Platform.SetRenderer (cell.View, this.view);
                //                    cell.View.IsPlatformEnabled = true;
                //                    this.Invalidate ();
                //                    return;
                //                }
                //                this.RemoveView (this.view.ViewGroup);
                //                Platform.SetRenderer (this.viewCell.View, null);
                //                this.viewCell.View.IsPlatformEnabled = false;
                //                this.view.ViewGroup.Dispose ();
                //                this.viewCell = cell;
                //                this.view = RendererFactory.GetRenderer (this.viewCell.View);
                //                Platform.SetRenderer (this.viewCell.View, this.view);
                //                this.AddView (this.view.ViewGroup);
            }

            /// <summary>
            ///     Called when [layout].
            /// </summary>
            /// <param name="changed">if set to <c>true</c> [changed].</param>
            /// <param name="l">The l.</param>
            /// <param name="t">The t.</param>
            /// <param name="r">The r.</param>
            /// <param name="b">The b.</param>
            protected override void OnLayout(bool changed, int l, int t, int r, int b)
            {
                var width = Context.FromPixels(r - l);
                var height = Context.FromPixels(b - t);
                _renderer.Element.Layout(new Rectangle(0, 0, width, height));
                _renderer.UpdateLayout();
            }

            //            protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
            //            {
            //                int size = View.MeasureSpec.GetSize (widthMeasureSpec);
            //                int measuredHeight;
            //               measuredHeight = (int)base.Context.ToPixels ((this.ParentRowHeight == -1) ? 44 : ((double)this.ParentRowHeight));

            //                base.SetMeasuredDimension (size, measuredHeight);
            //            }
        }
    }
}