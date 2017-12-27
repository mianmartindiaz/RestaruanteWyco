using System;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
namespace WyCo.Tabbed
{
    public class DefaultTemplate : AbsoluteLayout
    {
        public DefaultTemplate()
        {
            this.Padding = 3;
            this.HeightRequest = 100;

            var title = new Label { HorizontalTextAlignment = TextAlignment.Start, HorizontalOptions = LayoutOptions.StartAndExpand,TextColor =Color.Navy, FontAttributes = FontAttributes.Bold };
            var Descr = new Label { HorizontalTextAlignment = TextAlignment.Start, HorizontalOptions = LayoutOptions.FillAndExpand };
            var price = new Label { HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.End, TextColor=Color.Gray, FontSize=15, FontAttributes=FontAttributes.Bold };
            var euro = new Label { HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.End, TextColor = Color.Gray, FontSize = 15, FontAttributes = FontAttributes.Bold,Text="€" };
            var line = new BoxView {HeightRequest=3, Color=Color.Navy };


            Children.Add(title, new Rectangle(.24, 0.2, 0.9, 1.9), AbsoluteLayoutFlags.All);
            Children.Add(Descr, new Rectangle(.24, .7, 0.8, 0.8), AbsoluteLayoutFlags.All);
            Children.Add(price, new Rectangle(.90, 0.2, 0.5, 1.9), AbsoluteLayoutFlags.All);
            Children.Add(euro, new Rectangle(.95, 0.2, 0.5, 1.9), AbsoluteLayoutFlags.All);
            Children.Add(line,  new Rectangle(1, 1, 1, 0.1), AbsoluteLayoutFlags.All);

            title.SetBinding(Label.TextProperty, "Nombre");
            Descr.SetBinding(Label.TextProperty, "Descripcion");
            price.SetBinding(Label.TextProperty, "Precio");
        }
    }

    public class AccordionView : ScrollView
    {
        private StackLayout _layout = new StackLayout { Spacing = 1 };

        public DataTemplate Template { get; set; }
        public DataTemplate SubTemplate { get; set; }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                propertyName: "ItemsSource",
                returnType: typeof(IList),
                declaringType: typeof(AccordionSectionView),
                defaultValue: default(IList),
                propertyChanged: AccordionView.PopulateList);

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public AccordionView(DataTemplate itemTemplate)
        {
            this.SubTemplate = itemTemplate;
            this.Template = new DataTemplate(() => (new AccordionSectionView(itemTemplate, this)));
            this.Content = _layout;
        }

        void PopulateList()
        {
            _layout.Children.Clear();

            foreach (object item in this.ItemsSource)
            {
                var template = (View)this.Template.CreateContent();
                template.BindingContext = item;
                _layout.Children.Add(template);
            }
        }

        static void PopulateList(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            ((AccordionView)bindable).PopulateList();
        }
    }

    public class AccordionSectionView : StackLayout
    {
        private bool _isExpanded = false;
        private StackLayout _content = new StackLayout { HeightRequest = 0 };
        private ImageSource _arrowRight = ImageSource.FromFile("ic_keyboard_arrow_right_white_24dp.png");
        private ImageSource _arrowDown = ImageSource.FromFile("ic_keyboard_arrow_down_white_24dp.png");
        private StackLayout _header = new StackLayout();
        private Image _headerTitle = new Image { HorizontalOptions = LayoutOptions.FillAndExpand };
        private DataTemplate _template;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                propertyName: "ItemsSource",
                returnType: typeof(IList),
                declaringType: typeof(AccordionSectionView),
                defaultValue: default(IList),
                propertyChanged: AccordionSectionView.PopulateList);

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                propertyName: "Title",
                returnType: typeof(string),
                declaringType: typeof(AccordionSectionView),
                propertyChanged: AccordionSectionView.ChangeTitle);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public AccordionSectionView(DataTemplate itemTemplate, ScrollView parent)
        {
            _template = itemTemplate;
            _header.Children.Add(_headerTitle);

            this.Spacing = 0;
            this.Children.Add(_header);
            this.Children.Add(_content);

            _header.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    Command = new Command(async () =>
                    {
                        if (_isExpanded)
                        {
                            _content.HeightRequest = 0;
                            _content.IsVisible = false;
                            _isExpanded = false;
                        }
                        else
                        {
                            _content.HeightRequest = _content.Children.Count * 70;
                            _content.IsVisible = true;
                            _isExpanded = true;

                            // Scroll top by the current Y position of the section
                            if (parent.Parent is VisualElement)
                            {
                                await parent.ScrollToAsync(0, this.Y, true);
                            }
                        }
                    })
                }
            );
        }

        void ChangeTitle()
        {
            _headerTitle.Source = Title;
        }

        void PopulateList()
        {
            _content.Children.Clear();

            foreach (object item in this.ItemsSource)
            {
                var template = (View)_template.CreateContent();
                template.BindingContext = item;
                _content.Children.Add(template);
            }
        }

        static void ChangeTitle(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            ((AccordionSectionView)bindable).ChangeTitle();
        }

        static void PopulateList(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            ((AccordionSectionView)bindable).PopulateList();
        }
    }
}
